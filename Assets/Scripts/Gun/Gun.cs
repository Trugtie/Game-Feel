using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    public static Action OnFire;

    private static readonly int FIRE_HASH = Animator.StringToHash("Fire");

    public Transform BulletSpawnPoint => _bulletSpawnPoint;

    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _gunFireCD = 0.5f;

    private ObjectPool<Bullet> _bulletPool;

    private float _lastFireTime;

    private Animator _animator;

    private Vector3 _mousePos;

    private CinemachineImpulseSource _cinemachineImpulseSrc;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _cinemachineImpulseSrc = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        CreateBulletPool();
    }

    private void OnEnable()
    {
        OnFire += PlayGunAnim;
        OnFire += ActiveScreenShake;
    }

    private void OnDisable()
    {
        OnFire -= PlayGunAnim;
        OnFire -= ActiveScreenShake;
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

    private void CreateBulletPool()
    {
        _bulletPool = new ObjectPool<Bullet>(
            () => Instantiate(_bulletPrefab),
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(bullet.gameObject),false,20,40
            );
    }

    public void ReleaseBulletToPool(Bullet bullet)
    {
        _bulletPool.Release(bullet);
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
        Bullet newBullet = _bulletPool.Get();
        newBullet.Init(this,_bulletSpawnPoint.position, _mousePos);
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

    private void ActiveScreenShake()
    {
        _cinemachineImpulseSrc.GenerateImpulse();
    }
}
