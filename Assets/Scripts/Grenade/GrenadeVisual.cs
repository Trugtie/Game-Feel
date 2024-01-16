using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GrenadeVisual : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    [SerializeField] private ParticleSystem _explodeVFX;

    private void Start()
    {
        Grenade.OnExplode += Grenade_OnExplode;
    }

    private void Grenade_OnExplode(Grenade grenade)
    {
        Instantiate(_explodeVFX,grenade.transform.position,Quaternion.identity);
    }

    private void OnDestroy()
    {
        Grenade.OnExplode -= Grenade_OnExplode;
    }
}
