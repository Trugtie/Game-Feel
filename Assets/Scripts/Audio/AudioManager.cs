using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundSO _shootSO;
    [SerializeField] private SoundSO _jumpSO;

    private void OnEnable()
    {
        Gun.OnFire += Gun_OnFire;
        PlayerController.OnJump += PlayerController_OnJump;
    }

    private void OnDisable()
    {
        Gun.OnFire -= Gun_OnFire;
        PlayerController.OnJump -= PlayerController_OnJump;
    }

    private void PlaySound(SoundSO soundSO)
    {
        GameObject audioTempObject = new GameObject("Audio Temp Object");
        AudioSource audioSrc = audioTempObject.AddComponent<AudioSource>();
        audioSrc.clip = soundSO.AudioClip;
        audioSrc.Play();
    }

    private void Gun_OnFire()
    {
        PlaySound(_shootSO);
    }

    private void PlayerController_OnJump()
    {
        PlaySound(_jumpSO);
    }
}
