using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDeath;

    [SerializeField] private int _startingHealth = 3;

    [SerializeField] private GameObject _splatterPrefab;

    [SerializeField] private GameObject _deathVFXPrefab;

    private int _currentHealth;
   
    private void Start() {
        ResetHealth();
    }

    private void OnEnable()
    {
        OnDeath += SpawnSplatterPrefab;
        OnDeath += SpawnDeathVFX;
    }

    private void OnDisable()
    {
        OnDeath -= SpawnSplatterPrefab;
        OnDeath -= SpawnDeathVFX;
    }

    public void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int amount) {
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    private void SpawnSplatterPrefab()
    {
        GameObject splatterObject = Instantiate(_splatterPrefab, transform.position, transform.rotation);

        SpriteRenderer splatterSpriteRenderer = splatterObject.GetComponent<SpriteRenderer>();

        ColorChanger colorChanger = GetComponent<ColorChanger>();

        Color currentColor = colorChanger.DefaultColor;

        splatterSpriteRenderer.color = currentColor;
    }

    private void SpawnDeathVFX()
    {
        GameObject deathVFX = Instantiate(_deathVFXPrefab,transform.position, transform.rotation);

        ParticleSystem.MainModule ps = deathVFX.GetComponent<ParticleSystem>().main;

        ColorChanger colorChanger = GetComponent<ColorChanger>();
        Color currentColor = colorChanger.DefaultColor;

        ps.startColor = currentColor;
    }
}
