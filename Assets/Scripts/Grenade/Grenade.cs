using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public static Action<Grenade> OnExplode;
    public static Action<Grenade> OnBeep;

    [SerializeField] private float _explodeTimeMax = 3f;
    [SerializeField] private float _explodeRadiusMax = 1f;
    [SerializeField] private int _damgeAmount = 2;
    [SerializeField] private float _knockbackThurst = 20f;

    private float _exploreTimePerSecondMax;
    private float _exploreTimePerSecond;

    private float _explodeTimer;

    private void Awake()
    {
        _exploreTimePerSecondMax = (float)Math.Ceiling(_explodeTimeMax * 0.33f);
    }

    private void Update()
    {
        _explodeTimer += Time.deltaTime;

        if (_explodeTimer >= _explodeTimeMax)
        {
            Explode();
            OnExplode?.Invoke(this);
        }

        _exploreTimePerSecond += Time.deltaTime;

        if(_exploreTimePerSecond>= _exploreTimePerSecondMax)
        {
            OnBeep?.Invoke(this);
            _exploreTimePerSecond = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Explode();
            OnExplode?.Invoke(this);
        }
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explodeRadiusMax);

        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<IDamgeable>() != null)
            {
                IDamgeable iDamgeableObject = collider.gameObject.GetComponent<IDamgeable>();
                iDamgeableObject.TakeDamge(_damgeAmount, _knockbackThurst);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explodeRadiusMax);
    }
}
