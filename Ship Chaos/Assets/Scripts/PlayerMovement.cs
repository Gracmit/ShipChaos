using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _speed;
    
    private Vector3 _direction;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        _controller.Move(_direction.normalized * (_speed * Time.deltaTime));
        
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, _direction, Time.deltaTime * rotateSpeed);
    }
}
