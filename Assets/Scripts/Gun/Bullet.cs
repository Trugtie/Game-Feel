using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _bulletVFXPrefab;

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _knockbackThurst = 20f;

    private Gun _gun;

    private Vector2 _fireDirection;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(Gun gun,Vector2 bulletSpawnPos, Vector2 mousePos)
    {
        _gun = gun;
        transform.position = bulletSpawnPos;
        _fireDirection = (mousePos - bulletSpawnPos).normalized;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_bulletVFXPrefab, transform.position, Quaternion.identity);

        Ihitable iHitable = other.gameObject.GetComponent<Ihitable>();
        iHitable?.TakeHit();

        IDamgeable iDamgeable = other.gameObject.GetComponent<IDamgeable>();

        iDamgeable?.TakeDamge(_fireDirection, _damageAmount, _knockbackThurst);

        _gun.ReleaseBulletToPool(this);
    }
}