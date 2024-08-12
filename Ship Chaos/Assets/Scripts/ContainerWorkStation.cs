using System;
using UnityEngine;

public class ContainerWorkStation : BaseWorkStation
{
    [SerializeField] private LabObjectSO _labObjectSo;

    public override void Interact(Player player)
    {
        if (player.HasLabObject())
            return;
        
        LabObject.SpawnLabObject(_labObjectSo, player);
        
    }
}
