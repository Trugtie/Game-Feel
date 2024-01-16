using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundCollectionSO : ScriptableObject
{
    [Header("Music")]
    public SoundSO[] DiscoPartyMusic;
    public SoundSO[] FightMusic;

    [Header("SFX")]
    public SoundSO[] Shoot;
    public SoundSO[] Jump;
    public SoundSO[] Splat;
    public SoundSO[] Jetpack;
}
