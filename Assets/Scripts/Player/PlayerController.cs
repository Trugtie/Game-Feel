using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpStrength = 7f;

    [SerializeField] private Transform _feetPos;
    [SerializeField] private Vector2 _feetBoxSize;
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _movement;

    private Rigidbody2D _rigidBody;

    public void Awake() {
        if (Instance == null) { Instance = this; }

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GatherInput();
        Jump();
        HandleSpriteFlip();
    }

    private void FixedUpdate() {
        Move();
    }

    public bool IsFacingRight()
    {
        return transform.eulerAngles.y == 0;
    }

    private void GatherInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        _movement = new Vector2(moveX * _moveSpeed, _rigidBody.velocity.y);
    }

    private void Move() {

        _rigidBody.velocity = _movement;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckOnGround()) {
            _rigidBody.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
        }
    }

    private bool CheckOnGround()
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
