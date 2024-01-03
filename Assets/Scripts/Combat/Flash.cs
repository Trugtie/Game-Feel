using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _flashMaterial;

    [SerializeField] private float _flashTimerMax = 0.1f;

    private SpriteRenderer[] _spriteRenderes;

    void Start()
    {
        _spriteRenderes = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        foreach(SpriteRenderer sr in _spriteRenderes)
        {
            sr.material = _flashMaterial;
            sr.color = Color.white;
        }

        yield return new WaitForSeconds(_flashTimerMax);

        SetDefaultMaterial();
    }

    private void SetDefaultMaterial()
    {
        foreach (SpriteRenderer sr in _spriteRenderes)
        {
            sr.material = _defaultMaterial;
        }
    }


}
