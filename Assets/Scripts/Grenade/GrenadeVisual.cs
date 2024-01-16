using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GrenadeVisual : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    [SerializeField] private ParticleSystem _explodeVFX;
    [SerializeField] private float _lightLifeTime = 0.1f;

    private CinemachineImpulseSource _cinemachineImpulseSrc;

    private void Awake()
    {
        _cinemachineImpulseSrc = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        Grenade.OnExplode += Grenade_OnExplode;
        Grenade.OnBeep += Grenade_OnBeep;
    }

    private void Grenade_OnExplode(Grenade grenade)
    {
        Instantiate(_explodeVFX,grenade.transform.position,Quaternion.identity);
        _cinemachineImpulseSrc.GenerateImpulse();
    }

    private void Grenade_OnBeep(Grenade grenade)
    {
        StartCoroutine(TurnOffLightRoutine());
    }

    private IEnumerator TurnOffLightRoutine()
    {
        _light2D.gameObject.SetActive(true);
        yield return new WaitForSeconds(_lightLifeTime);
        _light2D.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Grenade.OnExplode -= Grenade_OnExplode;
        Grenade.OnBeep -= Grenade_OnBeep;
    }
}
