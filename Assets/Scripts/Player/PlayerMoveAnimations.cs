using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAnimations : MonoBehaviour
{
    [SerializeField] private ParticleSystem _playerDustVFX;
    [SerializeField] private ParticleSystem _poofDustVFX;

    private void OnEnable()
    {
        PlayerController.OnJump += PoofHandle;
    }

    private void OnDisable()
    {
        PlayerController.OnJump -= PoofHandle;
    }


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

    private void PoofHandle()
    {
        _poofDustVFX.Play();
    }
}
