using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public Action OnKnockbackStart;
    public Action OnKnockbackEnd;

    [SerializeField] private float _knockBackTime = .2f;

    private Rigidbody2D _rb;
    private Vector3 _knockbackDirection;
    private float _knockBackThurst;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OnKnockbackStart += ApplyKnockbackForce;
        OnKnockbackEnd += KnockbackEnd;
    }

    private void OnDisable()
    {
        OnKnockbackStart -= ApplyKnockbackForce;
        OnKnockbackEnd -= KnockbackEnd;
    }

    public void GetKnockback(Vector3 knockbackDir, float knockBackThurst)
    {
        _knockbackDirection = knockbackDir;
        _knockBackThurst = knockBackThurst;

        OnKnockbackStart?.Invoke();
    }

    private void ApplyKnockbackForce()
    {
        Vector3 knockbackDirection =  _knockbackDirection.normalized * _rb.mass * _knockBackThurst;
        _rb.AddForce(knockbackDirection, ForceMode2D.Impulse);

        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(_knockBackTime);
        OnKnockbackEnd?.Invoke();
    }

    private void KnockbackEnd()
    {
        _rb.velocity = Vector2.zero;
    }

}
