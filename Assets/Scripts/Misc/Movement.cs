using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;

    private float _moveX;

    private bool _canMove = true;

    private Knockback _knockback;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
    }

    private void OnEnable()
    {
        _knockback.OnKnockbackStart += CanMoveFalse;
        _knockback.OnKnockbackEnd += CanMoveTrue;
    }

    private void OnDisable()
    {
        _knockback.OnKnockbackStart -= CanMoveFalse;
        _knockback.OnKnockbackEnd -= CanMoveTrue;
    }

    private void CanMoveTrue()
    {
        _canMove = true;
    }

    private void CanMoveFalse()
    {
        _canMove = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_canMove) return;

        Vector3 moveDir = new Vector2(_moveSpeed * _moveX, _rb.velocity.y);

        _rb.velocity = moveDir;
    }

    public void SetCurrentDir(float currentDir)
    {
        _moveX = currentDir;
    }
}
