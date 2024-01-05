using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanAbility : MonoBehaviour
{
    [SerializeField] private float _tiltAngle = 14f;
    [SerializeField] private float _tileSpeed = 15f;

    [SerializeField] private Transform[] _spriteTransform;

    [SerializeField] private bool _oppositeLean = false;
    
    [SerializeField] private GameObject _leanAbleObject;

    private ILeanable _iLeanable;

    private void Awake()
    {
      _iLeanable = _leanAbleObject.GetComponent<ILeanable>();
    }


    private void Update()
    {
        ApplyTilt();
    }


    private void ApplyTilt()
    {
        float targetAngle;

        if (_iLeanable.MoveDir.x < 0f)
        {
            targetAngle = _tiltAngle;
        }
        else if (_iLeanable.MoveDir.x > 0f)
        {
            targetAngle = -_tiltAngle;
        }
        else
        {
            targetAngle = 0f;
        }

        if(_oppositeLean) { targetAngle=-targetAngle; }

        foreach(Transform spriteTrans in _spriteTransform)
        {
            Quaternion currentCharacterRotation = spriteTrans.rotation;
            Quaternion targetCharacterRotation = Quaternion.Euler(currentCharacterRotation.eulerAngles.x, currentCharacterRotation.eulerAngles.y, targetAngle);

            spriteTrans.rotation = Quaternion.Lerp(currentCharacterRotation, targetCharacterRotation, _tileSpeed * Time.deltaTime);
        }
    }
}
