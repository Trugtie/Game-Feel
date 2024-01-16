using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LauchGrenade : MonoBehaviour
{
    public static Action OnThrowGrenade;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private float _throwStrength = 10f;
    [SerializeField] private float _torqueStrength = 5f;

    private void OnEnable()
    {
        OnThrowGrenade += ThrowGrenade;
    }

    private void OnDisable()
    {
        OnThrowGrenade -= ThrowGrenade;
    }

    private void Update()
    {
        HandleThrowGrenade();
    }

    private void HandleThrowGrenade()
    {
        if (!_playerInput.FrameInput.ThrowGrenade) return;

        OnThrowGrenade?.Invoke();
    }

    private void ThrowGrenade()
    {
        GameObject grenadeObj = Instantiate(_grenadePrefab, transform.position, Quaternion.identity);

        Rigidbody2D grenadeRb = grenadeObj.GetComponent<Rigidbody2D>();

        Vector2 throwDir = (_playerInput.FrameInput.MousePos - (Vector2)transform.position).normalized;

        grenadeRb.AddForce(throwDir * _throwStrength,ForceMode2D.Force);
        grenadeRb.AddTorque(_torqueStrength, ForceMode2D.Force);
    }

}
