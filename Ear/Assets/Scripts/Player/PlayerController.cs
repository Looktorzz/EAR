using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DirectionPlayer
{
    North,
    East,
    West,
    South,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _moveSpeed;
    
    [SerializeField] private Interactor _interactor;
    [SerializeField] private Item _item;
    
    [Header("Test")]
    [SerializeField] private TestTrigger _testTrigger;
    [SerializeField] private GameObject _goTest;
    private bool isSet = true;

    private Rigidbody _rb;
    private InputSystems _input;
    private Vector2 _moveVector2;
    public bool isGrabItem = false;

    private void Awake()
    {
        _input = new InputSystems();
        _rb = GetComponent<Rigidbody>();
        // idle
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
        
        _input.Player.Run.started += OnRun;
        _input.Player.Run.canceled += OnRun;
        
        _input.Player.Interact.started += OnInteract;
        _input.Player.Interact.canceled += OnInteract;
        
        _input.Player.GrabItem.started += OnGrabItem;
        _input.Player.GrabItem.canceled += OnGrabItem;
    }
    
    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
        
        _input.Player.Run.started += OnRun;
        _input.Player.Run.canceled += OnRun;
        
        _input.Player.Interact.started += OnInteract;
        _input.Player.Interact.canceled += OnInteract;
        
        _input.Player.GrabItem.started += OnGrabItem;
        _input.Player.GrabItem.canceled += OnGrabItem;
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
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        // walk
        _moveVector2 = value.ReadValue<Vector2>();
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

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _interactor.PressInteract();
            
            /*Debug.Log("push push push");
            
            if (_testTrigger.isPlayerInArea)
            {
                isSet = !isSet;
                _goTest.SetActive(isSet);
            }*/
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
