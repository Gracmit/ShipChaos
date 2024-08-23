using System;
using UnityEngine;

public class BinocularsWorkStation : BaseWorkStation
{
    private CameraManager _cameraManager;

    private void Awake()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
    }

    public override void Interact(Player player)
    {
        if (player.HasLabObject()) return;
        
        _cameraManager.ChangeToSeaCamera();
    }
}
