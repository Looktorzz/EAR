using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnMove : State
{
    public OnMove(Interactor interactor, Item item, Hand hand, Rigidbody rb, InputSystems input, Animator anim, SpriteRenderer spriteRenderer) : base(interactor, item, hand, rb, input, anim, spriteRenderer)
    {
        _input = input;
        _rb = rb;
    }

    private InputSystems _input;
    private Vector2 _moveVector2;

    private Rigidbody _rb;

    private float _moveSpeed;

    
    
    public override void Enter()
    {
        Debug.Log("Enter");
        _input = new InputSystems();
        
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;

        base.Enter();
    }
    
    public override void Update()
    {
        Debug.Log(_rb);
        _rb.velocity = new Vector3(_moveVector2.x * _moveSpeed, 
            _rb.velocity.y, _moveVector2.y * _moveSpeed);
        
    }
    
    
    public override void Exit()
    {
        base.Exit();
    }
    
    
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        // walk
        _moveVector2 = value.ReadValue<Vector2>();
        // _animator.SetTrigger("Walking");
        
        if (_moveVector2.x < 0)
        {
            // Left
            _moveSpeed = 5.5f;
        }
        else if (_moveVector2.x > 0)
        {
            // Right
            _moveSpeed = 5.5f;
        }
        else if (_moveVector2.y > 0)
        {
            // Back
            _moveSpeed = 4.12f;
        }
        else if (_moveVector2.y < 0)
        {
            // Front
            _moveSpeed = 4.12f;
        }
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        // idle
        _moveVector2 = Vector2.zero;
        Anim.SetFloat("Horizontal", 0);
        Anim.SetFloat("Vertical", 0);
    }

}
