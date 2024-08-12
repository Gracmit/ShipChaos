using UnityEngine;

public class BaseWorkStation : MonoBehaviour, ILabObjectParent
{
    [SerializeField] private Transform _workStationTop;
    private LabObject _labObject;
    public virtual void Interact(Player player)
    {
        
    }

    public Transform GetLabObjectFollowTransform() => _workStationTop;

    public void SetLabObject(LabObject labObject) => _labObject = labObject;

    public LabObject GetLabObject() => _labObject;

    public void ClearLabObject() => _labObject = null;

    public bool HasLabObject() => _labObject != null;
}
