using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _playerDustVFX;

    private void Update()
    {
        DustVFXHandle();
    }

    private void DustVFXHandle()
    {
        if (PlayerController.Instance.CheckOnGround())
        {
            if (!_playerDustVFX.isPlaying)
            {
                _playerDustVFX.Play();
            }
        }
        else
        {
            if(_playerDustVFX.isPlaying)
            {
                _playerDustVFX.Stop();
            }
        }
    }
}
