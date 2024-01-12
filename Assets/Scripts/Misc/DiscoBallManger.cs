using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DiscoBallManger : MonoBehaviour
{
    public static Action OnDiscoBallHit;

    [SerializeField] private float _discoPartyTime = 2f;

    [SerializeField] private ColorSpotLight[] _colorSpotlights;

    [SerializeField] private Light2D _light2D;
    [SerializeField] private float _discoIntensity = .2f;

    private Coroutine _discoRoutine;

    private void OnEnable()
    {
        OnDiscoBallHit += DimTheLights;
    }

    private void OnDisable()
    {
        OnDiscoBallHit -= DimTheLights;
    }

    public void TriggerDiscoParty()
    {
        OnDiscoBallHit?.Invoke();
    }

    private void DimTheLights()
    {
        if (_discoRoutine != null) return;

        foreach(ColorSpotLight colorSpotLight in _colorSpotlights)
        {
            colorSpotLight.TriggerDiscoParty(_discoPartyTime);
        }

        _discoRoutine = StartCoroutine(DiscoRoutine());
    }

    private IEnumerator DiscoRoutine()
    {
        float defaultGlobalLightIntensity = _light2D.intensity;
        _light2D.intensity = _discoIntensity;

        yield return new WaitForSeconds(_discoPartyTime);

        _light2D.intensity = defaultGlobalLightIntensity;
        _discoRoutine = null;
    }
}
