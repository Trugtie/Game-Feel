using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static event EventHandler OnPlayerRespawn;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private Transform _spawnPostion;

    [SerializeField] private float _respawnTime;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private Coroutine _currentRoutine;

    private void OnEnable()
    {
        Health.OnDeath += Health_Ondeath;
    }

    private void OnDisable()
    {
        Health.OnDeath -= Health_Ondeath;
    }

    private void Health_Ondeath(Health health)
    {
        if (health.gameObject.GetComponent<PlayerController>() != null)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (_currentRoutine != null) return;

        _currentRoutine = StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(_respawnTime);

        GameObject playerObject = Instantiate(_playerPrefab, _spawnPostion.position, Quaternion.identity);

        _virtualCamera.Follow = playerObject.transform;

        OnPlayerRespawn?.Invoke(this, EventArgs.Empty);

        _currentRoutine = null;
    }
}
