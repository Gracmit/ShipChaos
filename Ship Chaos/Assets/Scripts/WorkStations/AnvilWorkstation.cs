using System.Collections.Generic;
using UnityEngine;

public class AnvilWorkstation : BaseWorkStation
{
    [SerializeField] private List<LabObjectSO> _acceptedLabObjects;
    [SerializeField] private LabObjectSO _output;
    [SerializeField] private int _progressAmount;

    private int _progress;
    public override void Interact(Player player)
    {
        if (!HasLabObject()) //No Lab object in the workstation
        {
            if (_acceptedLabObjects.Contains(player.GetLabObject().GetLabObjectSo))
            {
                player.GetLabObject().SetLabObjectParent(this);
                _progress = 0;
            }
        }
        else // Lab Object is in the workstation
        {
            if (player.HasLabObject()) return;
            GetLabObject().SetLabObjectParent(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!HasLabObject()) return;
        if (GetLabObject().GetLabObjectSo == _output) return;
        
        _progress++;
        Debug.Log($"Hit! Progress {_progress}/{_progressAmount}");

        if (_progress >= _progressAmount)
        {
            GetLabObject().DestroyLabObject();
            LabObject.SpawnLabObject(_output, this);
            _progress = 0;
        }
    }
}
