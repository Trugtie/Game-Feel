using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _flashMaterial;

    [SerializeField] private float _flashTimerMax = 0.1f;

    private SpriteRenderer[] _spriteRenderes;

    private ColorChanger _colorChanger;

    void Start()
    {
        _spriteRenderes = GetComponentsInChildren<SpriteRenderer>();
        _colorChanger = GetComponent<ColorChanger>();
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

            if(_colorChanger) _colorChanger.SetColor(Color.white);
        }

        yield return new WaitForSeconds(_flashTimerMax);

        SetDefaultMaterial();
    }

    private void SetDefaultMaterial()
    {
        foreach (SpriteRenderer sr in _spriteRenderes)
        {
            sr.material = _defaultMaterial;

            if (_colorChanger) _colorChanger.SetColor(_colorChanger.DefaultColor);
        }
    }


}
