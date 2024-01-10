using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpotLight : MonoBehaviour
{
    [SerializeField] private Transform _spotLightTransform;

    [SerializeField] private float _rotationSpeed = 20f;
    [SerializeField] private float _rotationAngleMax = 40f;

    private float _currentRotation;

    private void Awake()
    {
        RandomStartingRotation();
    }

    private void Update()
    {
        RotateSpotlight();
    }

    private void RotateSpotlight()
    {
        _currentRotation += _rotationSpeed * Time.deltaTime;

        float zAngle = Mathf.PingPong(_currentRotation, _rotationAngleMax);

        _spotLightTransform.localRotation = Quaternion.Euler(0f,0f, zAngle);
    }

    private void RandomStartingRotation()
    { 
        float randomAngleZ = Random.Range(-_rotationAngleMax, _rotationAngleMax);

        _spotLightTransform.localRotation = Quaternion.Euler(0f, 0f, randomAngleZ);

        _currentRotation = randomAngleZ + _rotationAngleMax;
    }
}
