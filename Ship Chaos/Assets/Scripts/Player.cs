using System;
using UnityEngine;

public class Player : MonoBehaviour, ILabObjectParent
{
    public static event Action<BaseWorkStation> SelectedCounterChanged; 

    [SerializeField] private int _speed;
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _handPosition;
    
    private Vector3 _moveDirection;
    private CharacterController _controller;
    private BaseWorkStation _selectedWorkStation;
    private LabObject _labObject;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
        HandleInteractAction();
    }

    private void HandleMovement()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.z = Input.GetAxisRaw("Vertical");

        _controller.SimpleMove(_moveDirection.normalized * (_speed * Time.deltaTime));

        float rotateSpeed = _rotationSpeed;
        transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * rotateSpeed);
    }
    
    private void HandleInteractions()
    {
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseWorkStation baseWorkStation))
            {
                if (baseWorkStation != _selectedWorkStation)
                {
                    SetSelectedCounter(baseWorkStation);
                }
            }                 
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    
    private void HandleInteractAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_selectedWorkStation == null) return;
            
            _selectedWorkStation.Interact(this);
        }
    }
    
    private void SetSelectedCounter(BaseWorkStation selectedWorkStation)
    {
        _selectedWorkStation = selectedWorkStation;
        
        SelectedCounterChanged?.Invoke(selectedWorkStation);
    }

    public Transform GetLabObjectFollowTransform() => _handPosition;

    public void SetLabObject(LabObject labObject)
    {
        _labObject = labObject;
    }

    public LabObject GetLabObject()
    {
        return _labObject;
    }

    public void ClearLabObject() => _labObject = null;

    public bool HasLabObject() => _labObject != null;
}
