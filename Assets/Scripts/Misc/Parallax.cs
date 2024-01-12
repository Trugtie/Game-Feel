using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = 0.9f;

    private Camera _mainCamera;

    private Vector2 _startPos;

    private Vector2 _travelVector => (Vector2)_mainCamera.transform.position - _startPos;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 newPos = _startPos + new Vector2(_travelVector.x * _parallaxOffset, 0);
        transform.position = new Vector2(newPos.x, transform.position.y);
    }
}
