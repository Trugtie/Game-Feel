using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject SpaltterPrefab => _splatterPrefab;
    public GameObject DeathVFXPrefab=> _deathVFXPrefab;

    public static Action<Health> OnDeath;

    [SerializeField] private int _startingHealth = 3;

    [SerializeField] private GameObject _splatterPrefab;

    [SerializeField] private GameObject _deathVFXPrefab;

    private int _currentHealth;
   
    private void Start() {
        ResetHealth();
    }

    public void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int amount) {
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
