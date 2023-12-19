using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public static Action OnFire;

    private static readonly int FIRE_HASH = Animator.StringToHash("Fire");

    public Transform BulletSpawnPoint => _bulletSpawnPoint;

    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _gunFireCD = 0.5f;

    private float _lastFireTime;

    private Animator _animator;

    private Vector3 _mousePos;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        OnFire += PlayGunAnim;
    }

    private void OnDisable() {
         OnFire -= PlayGunAnim;
    }

    private void Update()
    {
        _lastFireTime -= Time.deltaTime;

        if (_lastFireTime <= 0f)
        {
            Shoot();
            _lastFireTime = _gunFireCD;
        }

        RotateGun();
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            ShootProjectile();
            OnFire?.Invoke();
        }
    }

    private void ShootProjectile()
    {
        Bullet newBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        newBullet.Init(_bulletSpawnPoint.position, _mousePos);
    }

    private void RotateGun()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = PlayerController.Instance.transform.InverseTransformPoint(_mousePos);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void PlayGunAnim()
    {
        _animator.Play(FIRE_HASH, 0, 0f);
    }
}
