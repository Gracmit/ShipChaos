using System.Collections.Generic;
using UnityEngine;

public class IronFurnaceWorkStation : BaseWorkStation
{
    [SerializeField] private List<LabObjectSO> _acceptedLabObjects;
    [SerializeField] private LabObjectSO _output;
    [SerializeField] private float _burningTime;

    private FurnaceState _state;
    private float _burnTimer;
    private List<LabObjectSO> _labObjectsInsideFurnace;

    private enum FurnaceState
    {
        Waiting,
        Running,
        Finished
    }
    
    private void Awake()
    {
        _state = FurnaceState.Waiting;
        _labObjectsInsideFurnace = new List<LabObjectSO>();
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

                var labObject = player.GetLabObject();
                if (_acceptedLabObjects.Contains(labObject.GetLabObjectSo))
                {
                    if (_labObjectsInsideFurnace.Contains(labObject.GetLabObjectSo)) return;
                    
                    _labObjectsInsideFurnace.Add(labObject.GetLabObjectSo);
                    labObject.DestroyLabObject();
                    player.ClearLabObject();

                    if (_labObjectsInsideFurnace.Count == 2)
                    {
                        _state = FurnaceState.Running;
                        _burnTimer = 0;
                        _labObjectsInsideFurnace.Clear();
                    }
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
