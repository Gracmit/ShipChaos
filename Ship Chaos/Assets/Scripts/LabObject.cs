using UnityEngine;

public class LabObject : MonoBehaviour
{
    [SerializeField] private LabObjectSO _labObjectSo;
    private ILabObjectParent _labObjectParent;
    private FollowTransform _followTransform;

    public LabObjectSO GetLabObjectSo => _labObjectSo;

    private void Awake()
    {
        _followTransform = GetComponent<FollowTransform>();
    }

    public static void SpawnLabObject(LabObjectSO labObjectSo, ILabObjectParent parent)
    {
        var instance = Instantiate(labObjectSo.prefab);
        var labObject = instance.GetComponent<LabObject>();
        parent.SetLabObject(labObject);
        labObject.SetLabObjectParent(parent);
        labObject._labObjectSo = labObjectSo;
    }

    public void SetLabObjectParent(ILabObjectParent parent)
    {
        if (_labObjectParent != null)
        {
            _labObjectParent.ClearLabObject();
        }

        _labObjectParent = parent;
        _followTransform.SetTargetTransform(parent.GetLabObjectFollowTransform());

        if (_labObjectParent.HasLabObject()) return;
        
        _labObjectParent.SetLabObject(this);
    }

    public void DestroyLabObject()
    {
        Destroy(gameObject);
    }

    public void SetFollowTransform(Transform followTransform)
    {
        _followTransform.SetTargetTransform(followTransform);
    }
}