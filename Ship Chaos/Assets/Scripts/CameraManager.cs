using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _labCamera;
    [SerializeField] private CinemachineVirtualCamera _seaCamera;

    private CinemachineVirtualCamera _activeCamera;

    private void Awake()
    {
        _activeCamera = _labCamera;
    }

    public void ChangeToSeaCamera()
    {
        _activeCamera.Priority = 0;
        _seaCamera.Priority = 1;
    }
}