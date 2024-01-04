using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSplatterHandler : MonoBehaviour
{
    private void OnEnable()
    {
        Health.OnDeath += SpawnSplatterPrefab;
        Health.OnDeath += SpawnDeathVFX;
    }

    private void OnDisable()
    {
        Health.OnDeath -= SpawnSplatterPrefab;
        Health.OnDeath -= SpawnDeathVFX;
    }

    private void SpawnSplatterPrefab(Health sender)
    {
        GameObject splatterObject = Instantiate(sender.SpaltterPrefab, sender.transform.position, transform.rotation);

        SpriteRenderer splatterSpriteRenderer = splatterObject.GetComponent<SpriteRenderer>();

        ColorChanger colorChanger = sender.GetComponent<ColorChanger>();

        Color currentColor = colorChanger.DefaultColor;

        splatterSpriteRenderer.color = currentColor;

        splatterObject.transform.parent = this.transform;
    }

    private void SpawnDeathVFX(Health sender)
    {
        GameObject deathVFX = Instantiate(sender.DeathVFXPrefab, sender.transform.position, transform.rotation);

        ParticleSystem.MainModule ps = deathVFX.GetComponent<ParticleSystem>().main;

        ColorChanger colorChanger = sender.GetComponent<ColorChanger>();
        Color currentColor = colorChanger.DefaultColor;

        ps.startColor = currentColor;

        deathVFX.transform.parent = this.transform;
    }
}
