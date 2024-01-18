using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    public event EventHandler<OnScoreChangeEventArgs> OnScoreChange;

    public class OnScoreChangeEventArgs : EventArgs
    {
        public int score;
    }

    public static Score Instance { get; private set; }

    private int _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        Health.OnDeath += Health_OnDeath;
        RespawnManager.OnPlayerRespawn += RespawnManager_OnPlayerRespawn;
    }

    private void RespawnManager_OnPlayerRespawn(object sender, EventArgs e)
    {
        ResetScore();
    }

    private void Health_OnDeath(Health sender)
    {
        if (sender.gameObject.GetComponent<Enemy>() != null)
        {
            AddScore(1);
        }
    }

    private void AddScore(int score)
    {
        _score += score;
        OnScoreChange?.Invoke(this, new OnScoreChangeEventArgs { score = _score });
    }

    private void ResetScore()
    {
        _score = 0;
        OnScoreChange?.Invoke(this, new OnScoreChangeEventArgs { score = _score });
    }

    private void OnDestroy()
    {
        Health.OnDeath -= Health_OnDeath;
        RespawnManager.OnPlayerRespawn -= RespawnManager_OnPlayerRespawn;
    }
}
