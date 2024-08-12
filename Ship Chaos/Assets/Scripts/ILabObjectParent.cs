using UnityEngine;

public interface ILabObjectParent
{
    public Transform GetLabObjectFollowTransform();
    
    public void SetLabObject(LabObject labObject);
    
    public LabObject GetLabObject();

    public void ClearLabObject();

    public bool HasLabObject();
}
