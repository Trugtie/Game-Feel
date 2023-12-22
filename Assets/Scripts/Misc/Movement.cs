using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;

    private float _moveX;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDir = new Vector2(_moveSpeed * _moveX, _rb.velocity.y);

        _rb.velocity = moveDir;
    }

    public void SetCurrentDir(float currentDir)
    {
        _moveX = currentDir;
    }
}
