using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image _blackScreenImage;

    [SerializeField] private float _fadeTimeMax = .3f;

    private void OnEnable()
    {
        Health.OnDeath += Health_OnDeath;
        RespawnManager.OnPlayerRespawn += RespawnManager_OnPlayerRespawn;
    }

    private void OnDisable()
    {
        Health.OnDeath -= Health_OnDeath;
        RespawnManager.OnPlayerRespawn -= RespawnManager_OnPlayerRespawn;
    }


    private void RespawnManager_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        FadeOut();
    }

    private void Health_OnDeath(Health sender)
    {
        if (sender.gameObject.GetComponent<PlayerController>() != null)
        {
            FadeIn();
        }
    }

    private void FadeIn()
    {
        StartCoroutine(FadeRoutine(1));
    }

    private void FadeOut()
    {
        StartCoroutine(FadeRoutine(0));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float elapsedTime = 0f;
        float startValue = _blackScreenImage.color.a;

        while(elapsedTime < _fadeTimeMax)
        {
            elapsedTime+= Time.deltaTime;

            float newAlpha = Mathf.Lerp(startValue, targetAlpha, elapsedTime / _fadeTimeMax);
            _blackScreenImage.color = new Color(_blackScreenImage.color.r, _blackScreenImage.color.g, _blackScreenImage.color.b, newAlpha);
            
            yield return null;
        }

        _blackScreenImage.color = new Color(_blackScreenImage.color.r, _blackScreenImage.color.g, _blackScreenImage.color.b, targetAlpha);
    }

}
