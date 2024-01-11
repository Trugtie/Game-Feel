using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundCollectionSO : ScriptableObject
{
    public SoundSO[] Shoot;
    public SoundSO[] Jump;
    public SoundSO[] Splat;
}
