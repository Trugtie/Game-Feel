using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnJump;

    public static PlayerController Instance;

    [SerializeField] private float _jumpStrength = 7f;

    [SerializeField] private Transform _feetPos;
    [SerializeField] private Vector2 _feetBoxSize;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _extraGravity = 700f;
    [SerializeField] private float _gravityCustomDelayTimerMax = .2f;
    [SerializeField] private float _coyoteTimerMax = 0.5f;

    private float _gravityCustomDelayTimer;

    private bool _doubleJumpAvailable;

    private float _coyoteTimer;


    private Rigidbody2D _rigidBody;

    private PlayerInput _playerInput;
    private FrameInput _frameInput;

    private Movement _movement;

    public void Awake() {
        if (Instance == null) { Instance = this; }

        _rigidBody = GetComponent<Rigidbody2D>();

        _playerInput = GetComponent<PlayerInput>();

        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        OnJump += ApplyJumpForce;
    }

    private void OnDisable()
    {
        OnJump -= ApplyJumpForce;
    }

    private void Update()
    {
        GatherInput();
        Movement();
        CoyoteTimer();
        HandleJump();
        CalculateGravityCustomDelayTime();
        HandleSpriteFlip();
    }

    private void FixedUpdate()
    {
        AddExtraGravity();
    }

    private void CalculateGravityCustomDelayTime()
    {
        if (!CheckOnGround())
        {
            _gravityCustomDelayTimer += Time.deltaTime;
        }
        else
        {
            _gravityCustomDelayTimer = 0f;
        }
    }

    private void AddExtraGravity()
    {
        if (_gravityCustomDelayTimer > _gravityCustomDelayTimerMax)
        {
            _rigidBody.AddForce(new Vector2(0, -_extraGravity*Time.deltaTime));
        }
    }

    public bool IsFacingRight()
    {
        return transform.eulerAngles.y == 0;
    }

    private void GatherInput()
    {
        _frameInput = _playerInput.FrameInput;
    }

    private void Movement() {

        _movement.SetCurrentDir(_frameInput.Move.x);
    }

    private void HandleJump()
    {
        if (!_frameInput.Jump) return;

        if (CheckOnGround()) {
            OnJump?.Invoke();
        }
        else if (_coyoteTimer > 0f)
        {
            OnJump?.Invoke();
        }
        else if (_doubleJumpAvailable)
        {
            _doubleJumpAvailable = false;
            OnJump?.Invoke();
        }
    }

    private void CoyoteTimer()
    {
        if (CheckOnGround())
        {
            _doubleJumpAvailable = true;
            _coyoteTimer = _coyoteTimerMax;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }
    }

    private void ApplyJumpForce()
    {
        _rigidBody.velocity = Vector2.zero;
        _gravityCustomDelayTimer = 0f;
        _coyoteTimer = 0f;
        _rigidBody.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
    }

    public bool CheckOnGround()
    {
        bool isGrounded = Physics2D.OverlapBox(_feetPos.position, _feetBoxSize, 0f, _groundLayer);
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_feetPos.position, _feetBoxSize);
    }

    private void HandleSpriteFlip()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    } 
}
