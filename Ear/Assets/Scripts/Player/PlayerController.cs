using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public enum DirectionPlayer
{
    East,
    West,
    North,
    South,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveVector2;
    
    private Interactor _interactor;
    private Item _item;
    private Hand _hand;
    private Rigidbody _rb;
    private InputSystems _input;
    private Animator _animator;

    private bool _isHoldInteract = false;
    public bool isGrabItem = false;

    private string _animName;
    private string _animFreeze;
    private int _handDirection;
    private int _handFreeze;
    public bool isFreezeHand = false;

    private void Awake()
    {
        _input = new InputSystems();
        _rb = GetComponent<Rigidbody>();
        _interactor = GetComponent<Interactor>();
        _item = GetComponent<Item>();
        _hand = GetComponent<Hand>();
        _animator = GetComponentInChildren<Animator>();
        // idle

        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;

        _input.Player.Run.started += OnRun;
        _input.Player.Run.canceled += OnRun;


        _input.Player.InteractHold.started += OnInteractHoldStart;
        _input.Player.InteractHold.performed += OnInteractHoldPerformed;
        _input.Player.InteractHold.canceled += OnInteractHoldCanceled;


        _input.Player.GrabItem.started += OnGrabItem;
        _input.Player.GrabItem.canceled += OnGrabItem;
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    
    private void OnDisable()
    {
        _input.Disable();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_moveVector2.x * _moveSpeed, 
            _rb.velocity.y, _moveVector2.y * _moveSpeed);
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
            _handDirection = (int) DirectionPlayer.West;
            
            if (!_spriteRenderer.flipX && !isFreezeHand)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else if (_moveVector2.x > 0)
        {
            // Right
            _moveSpeed = 5.5f;
            _handDirection = (int) DirectionPlayer.East;
            
            if (_spriteRenderer.flipX && !isFreezeHand)
            {
                _spriteRenderer.flipX = false;
            }
        }
        else if (_moveVector2.y > 0)
        {
            // Back
            _moveSpeed = 4.12f;
            _handDirection = (int) DirectionPlayer.North;
        }
        else if (_moveVector2.y < 0)
        {
            // Front
            _moveSpeed = 4.12f;
            _handDirection = (int) DirectionPlayer.South;
        }

        if (!isFreezeHand)
        {
            // Wait animation
            // _animFreeze = _animName;
            // _animator.SetTrigger(_animName);
            
            _handFreeze = _handDirection;
            _hand.SentDirection(_handFreeze);
        }
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        // idle
        _moveVector2 = Vector2.zero;
    }
    
    private void OnRun(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            // run
            _moveSpeed = 10;
        }
        else
        {
            // walk
            _moveSpeed = 5.5f;
        }
    }

    private void OnInteractHoldStart(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            //Debug.LogWarning("IT's JUST PRESS BY HOLD BUTTON");
        }

    }

    private void OnInteractHoldPerformed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if(context.interaction is HoldInteraction)
            {
                _interactor.HoldInteract();
                Debug.LogWarning("IT's REALLY HOLD");
                _isHoldInteract = true;
            }  
        }
        
    }


    private void OnInteractHoldCanceled(InputAction.CallbackContext context)
    {
        if (context.interaction is PressInteraction) // Just Press No Hold Interact!
        {
            Debug.LogWarning("IT's Just PRESS");
            _interactor.PressInteract();
            return;
        }
        if (_isHoldInteract)
        {
            if (!context.ReadValueAsButton()) Debug.Log("Released");
            _interactor.ReleasedHoldInteract();
            _isHoldInteract = false;
        }
        
    }
    
    private void OnGrabItem(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("OnGrabItem Work!");
            
            if (!isGrabItem)
            {
                // Hold item
                _item.HoldItem();
            }
            else
            {
                // Place item
                _item.PlaceItem();
            }
        }
    }
}
