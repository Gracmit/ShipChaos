using System.Collections.Generic;
using UnityEngine;

public class IronFurnaceWorkStation : BaseWorkStation
{
    [SerializeField] private List<LabObjectSO> _acceptedLabObjects;
    [SerializeField] private LabObjectSO _output;
    [SerializeField] private float _burningTime;

    private FurnaceState _state;
    private float _burnTimer;

    private enum FurnaceState
    {
        Waiting,
        Running,
        Finished
    }
    
    private void Awake()
    {
        _state = FurnaceState.Waiting;
    }

    private void Update()
    {
        if (_state == FurnaceState.Running)
        {
            _burnTimer += Time.deltaTime;
            Debug.Log($"{_burnTimer} / {_burningTime}");
            if (_burnTimer >= _burningTime) _state = FurnaceState.Finished;
        }
    }

    public override void Interact(Player player)
    {
        switch (_state)
        {
            case FurnaceState.Waiting:
                if (!player.HasLabObject()) return;

                if (_acceptedLabObjects.Contains(player.GetLabObject().GetLabObjectSo))
                {
                    player.GetLabObject().DestroyLabObject();
                    player.ClearLabObject();
                    _state = FurnaceState.Running;
                    _burnTimer = 0;
                } 
                break;
            
            case FurnaceState.Running:
                break;
            
            case FurnaceState.Finished:
                if(player.HasLabObject()) return;
                
                LabObject.SpawnLabObject(_output, player);
                break;
        }
    }
}
