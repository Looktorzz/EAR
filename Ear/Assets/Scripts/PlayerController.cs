using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _moveSpeed;

    private Rigidbody _rb;
    private InputSystems _input;
    private Vector2 _moveVector2;

    private void Awake()
    {
        _input = new InputSystems();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
    }
    
    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_moveVector2.x * _moveSpeed, 
            _rb.velocity.y, _moveVector2.y * _moveSpeed);
        
        if (!_spriteRenderer.flipX && _moveVector2.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_spriteRenderer.flipX && _moveVector2.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        Debug.Log(_moveVector2);
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _moveVector2 = value.ReadValue<Vector2>();
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        _moveVector2 = Vector2.zero;
    }
}
