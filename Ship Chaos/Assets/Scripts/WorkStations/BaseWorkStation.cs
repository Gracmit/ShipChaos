
using UnityEngine;

public class BaseWorkStation : MonoBehaviour, ILabObjectParent
{
    [SerializeField] protected Transform _workStationTopPosition;
    private LabObject _labObject;
    

    public virtual void Interact(Player player)
    {
    }

    public virtual void InteractAlternate(Player player)
    {
    }
    
    public Transform GetLabObjectFollowTransform() => _workStationTopPosition;

    public void SetLabObject(LabObject labObject) => _labObject = labObject;

    public virtual LabObject GetLabObject()
    {
        return _labObject;
    }

    public void ClearLabObject() => _labObject = null;

    public bool HasLabObject()
    {
        return _labObject != null;
    }
}
