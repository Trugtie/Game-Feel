using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAnimations : MonoBehaviour
{
    [SerializeField] private ParticleSystem _playerDustVFX;
    [SerializeField] private ParticleSystem _poofDustVFX;

    [SerializeField] private float _yLandVelocityCheck = -20f;

    private Vector2 _velocityBeforePhysicUpdate;

    private CinemachineImpulseSource _impulseSource;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerController.OnJump += PoofHandle;
    }

    private void OnDisable()
    {
        PlayerController.OnJump -= PoofHandle;
    }

    private void FixedUpdate()
    {
        _velocityBeforePhysicUpdate = _rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_velocityBeforePhysicUpdate.y < _yLandVelocityCheck)
        {
            PoofHandle();
            _impulseSource.GenerateImpulse();
        }
    }

    private void Update()
    {
        DustVFXHandle();
    }

    private void DustVFXHandle()
    {
        if (PlayerController.Instance.CheckOnGround())
        {
            if (!_playerDustVFX.isPlaying)
            {
                _playerDustVFX.Play();
            }
        }
        else
        {
            if(_playerDustVFX.isPlaying)
            {
                _playerDustVFX.Stop();
            }
        }
    }

    private void PoofHandle()
    {
        _poofDustVFX.Play();
    }
}
