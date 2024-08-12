using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<BaseCounter> SelectedCounterChanged; 

    [SerializeField] private int _speed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private LayerMask _countersLayerMask;
    
    private Vector3 _moveDirection;
    private CharacterController _controller;
    private Vector3 _lastInteractionDirection;
    private BaseCounter _selectedCounter;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");

        _controller.SimpleMove(_moveDirection.normalized * (_speed * Time.deltaTime));

        float rotateSpeed = rotationSpeed;
        transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * rotateSpeed);
    }
    
    private void HandleInteractions()
    {
        
        if (_moveDirection != Vector3.zero)
            _lastInteractionDirection = _moveDirection;
        
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractionDirection, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
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
    
    
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        
        SelectedCounterChanged?.Invoke(selectedCounter);
    }
}
