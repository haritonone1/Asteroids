using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;
        
        var spaceshipPos = new Vector3(_target.position.x,_target.position.y,_cameraTransform.position.z);
        var currentCameraPosition = new Vector3(_cameraTransform.position.x,_cameraTransform.position.y,_cameraTransform.position.z);
        transform.position = Vector3.Lerp(currentCameraPosition, spaceshipPos, Time.deltaTime * _speed);
    }
}
