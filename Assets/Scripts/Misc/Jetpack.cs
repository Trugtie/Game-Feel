using System;
using System.Collections;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public static Action OnJetpack;

    [SerializeField] private GameObject _playerObject;

    [SerializeField] private TrailRenderer _jetpackTrailRenderer;

    [SerializeField] private float _jetpackMaxTime = 1f;
    [SerializeField] private float _jetpackStrenght = 10f;
    [SerializeField] private float _maxJetpackSpeed = 20f;

    private FrameInput _frameInput;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;

    private Coroutine _jetpackRoutine;

    private void Awake()
    {
        _playerInput= _playerObject.GetComponent<PlayerInput>();
        _rigidbody = _playerObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OnJetpack += StartJetpack;
    }

    private void OnDisable()
    {
        OnJetpack -= StartJetpack;
    }

    private void Update()
    {
        GatherInput();
        HandleJetpack();
    }

    private void GatherInput()
    {
        _frameInput = _playerInput.FrameInput;
    }

    private void HandleJetpack()
    {
        if (!_frameInput.Jetpack || _jetpackRoutine != null) return;

        OnJetpack?.Invoke();
    }

    private void StartJetpack()
    {
        _jetpackTrailRenderer.emitting = true;
        _jetpackRoutine = StartCoroutine(JetpackRoutine());
    }

    private IEnumerator JetpackRoutine()
    {
        float jetpackTime = 0;

        while (jetpackTime < _jetpackMaxTime)
        {
            jetpackTime+= Time.deltaTime;

            _rigidbody.AddForceY(_jetpackStrenght, ForceMode2D.Force);
            if (_rigidbody.velocity.y > _maxJetpackSpeed)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _maxJetpackSpeed);
            }

            yield return null;
        }

        _jetpackTrailRenderer.emitting = false;
        _jetpackRoutine = null;
    }

}
