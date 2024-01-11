using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float _masterVolume = 1f;

    [SerializeField] private SoundCollectionSO _soundCollection;

    private void OnEnable()
    {
        Gun.OnFire += Gun_OnFire;
        PlayerController.OnJump += PlayerController_OnJump;
        Health.OnDeath += Healt_OnDeath;
    }

    private void OnDisable()
    {
        Gun.OnFire -= Gun_OnFire;
        PlayerController.OnJump -= PlayerController_OnJump;
        Health.OnDeath -= Healt_OnDeath;
    }

    private void Gun_OnFire()
    {
        PlayRandomSound(_soundCollection.Shoot);
    }

    private void PlayerController_OnJump()
    {
        PlayRandomSound(_soundCollection.Jump);
    }

    private void Healt_OnDeath(Health health)
    {
        PlayRandomSound(_soundCollection.Splat);
    }

    private void PlayRandomSound(SoundSO[] soundsSO)
    {
        if (soundsSO != null && soundsSO.Length > 0)
        {
            SoundSO soundSO = soundsSO[Random.Range(0, soundsSO.Length)];
            SoundToPlay(soundSO);
        }
    }

    private void SoundToPlay(SoundSO soundSO)
    {
        AudioClip audioClip = soundSO.AudioClip;
        float volume = soundSO.Volume * _masterVolume;
        float pitch = soundSO.Pitch;
        bool loop = soundSO.Loop;

        if (soundSO.RandomizePitch)
        {
            float randomPitchModifier = Random.Range(-soundSO.RandomPitchRangeModifier, soundSO.RandomPitchRangeModifier);
            pitch = soundSO.Pitch + randomPitchModifier;
        }

        PlaySound(audioClip, volume, pitch, loop);
    }

    private void PlaySound(AudioClip clip,float volume, float pitch, bool loop)
    {
        GameObject audioTempObject = new GameObject("Audio Temp Object");
        AudioSource audioSrc = audioTempObject.AddComponent<AudioSource>();
        audioSrc.clip = clip;
        audioSrc.volume = volume;
        audioSrc.pitch = pitch;
        audioSrc.loop=loop;
        audioSrc.Play();

        if (!loop) { Destroy(audioTempObject, clip.length); }
    }
}
