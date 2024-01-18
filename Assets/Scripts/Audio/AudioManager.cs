using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float _masterVolume = 1f;

    [SerializeField] private SoundCollectionSO _soundCollection;

    [SerializeField] private AudioMixerGroup _sfxAudioMixerGroup;
    [SerializeField] private AudioMixerGroup _musicAudioMixerGroup;

    private AudioSource _currentMusic;

    #region Unity Methods
    private void Start()
    {
        PlayFightMusic();
    }

    private void OnEnable()
    {
        Gun.OnFire += Gun_OnFire;
        PlayerController.OnJump += PlayerController_OnJump;
        Health.OnDeath += Healt_OnDeath;
        DiscoBallManger.OnDiscoBallHit += PlayDiscoPartyMusic;
        Jetpack.OnJetpack+= Jetpack_OnJetpack;
        Grenade.OnExplode += Grenade_OnExplode;
        Grenade.OnBeep+= Grenade_OnBeep;
        LauchGrenade.OnThrowGrenade += LauchGrenade_OnThrowGrenade;
        Enemy.OnPlayerHit+= Enemy_OnPlayerHit;
    }

    private void OnDisable()
    {
        Gun.OnFire -= Gun_OnFire;
        PlayerController.OnJump -= PlayerController_OnJump;
        Health.OnDeath -= Healt_OnDeath;
        DiscoBallManger.OnDiscoBallHit -= PlayDiscoPartyMusic;
        Jetpack.OnJetpack -= Jetpack_OnJetpack;
        Grenade.OnExplode -= Grenade_OnExplode;
        Grenade.OnBeep -= Grenade_OnBeep;
        LauchGrenade.OnThrowGrenade -= LauchGrenade_OnThrowGrenade;
        Enemy.OnPlayerHit -= Enemy_OnPlayerHit;
    }
    #endregion

    #region Sound Methods
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
        AudioMixerGroup audioMixerGroup;

        pitch = RandomizePitch(soundSO, pitch);
        audioMixerGroup = DetermineAudioMixerGroup(soundSO);

        PlaySound(audioClip, volume, pitch, loop, audioMixerGroup);
    }

    private AudioMixerGroup DetermineAudioMixerGroup(SoundSO soundSO)
    {
        AudioMixerGroup audioMixerGroup;
        switch (soundSO.AudioType)
        {
            case SoundSO.AudioTypes.SFX:
                audioMixerGroup = _sfxAudioMixerGroup;
                break;
            case SoundSO.AudioTypes.Music:
                audioMixerGroup = _musicAudioMixerGroup;
                break;
            default:
                audioMixerGroup = null;
                break;
        }

        return audioMixerGroup;
    }

    private static float RandomizePitch(SoundSO soundSO, float pitch)
    {
        if (soundSO.RandomizePitch)
        {
            float randomPitchModifier = Random.Range(-soundSO.RandomPitchRangeModifier, soundSO.RandomPitchRangeModifier);
            pitch = soundSO.Pitch + randomPitchModifier;
        }

        return pitch;
    }

    private void PlaySound(AudioClip clip, float volume, float pitch, bool loop, AudioMixerGroup audioMixerGroup)
    {
        GameObject audioTempObject = new GameObject("Audio Temp Object");
        AudioSource audioSrc = audioTempObject.AddComponent<AudioSource>();
        audioSrc.clip = clip;
        audioSrc.volume = volume;
        audioSrc.pitch = pitch;
        audioSrc.loop = loop;
        audioSrc.outputAudioMixerGroup = audioMixerGroup;

        audioSrc.Play();

        if (!loop) { Destroy(audioTempObject, clip.length); }

        DetermineSound(audioMixerGroup, audioSrc);
    }

    private void DetermineSound(AudioMixerGroup audioMixerGroup, AudioSource audioSrc)
    {
        if (audioMixerGroup == _musicAudioMixerGroup)
        {
            if (_currentMusic != null)
            {
                _currentMusic.Stop();
            }

            _currentMusic = audioSrc;
        }
    }
    #endregion

    #region SFX
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

    private void Jetpack_OnJetpack()
    {
        PlayRandomSound(_soundCollection.Jetpack);
    }

    private void LauchGrenade_OnThrowGrenade()
    {
        PlayRandomSound(_soundCollection.GrenadeShoot);
    }

    private void Grenade_OnExplode(Grenade grenade)
    {
        PlayRandomSound(_soundCollection.GrenadeExplode);
    }

    private void Grenade_OnBeep(Grenade grenade)
    {
        PlayRandomSound(_soundCollection.GrenadeBeep);
    }

    private void Enemy_OnPlayerHit()
    {
        PlayRandomSound(_soundCollection.PlayerHit);
    }

    #endregion

    #region Music
    private void PlayDiscoPartyMusic()
    {
        PlayRandomSound(_soundCollection.DiscoPartyMusic);
        float discoPartyMusicLength = _soundCollection.DiscoPartyMusic[0].AudioClip.length;
        Utils.RunAfterDelay(this, discoPartyMusicLength, PlayFightMusic);
    }

    private void PlayFightMusic()
    {
        PlayRandomSound(_soundCollection.FightMusic);
    }
    #endregion
}
