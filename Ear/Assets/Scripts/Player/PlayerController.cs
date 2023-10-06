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
    public bool _isCanCrouching = false;


    private int _handDirection;
    public int handFreeze;
    public bool isFreezeHand = false;
    
    //Check Status
    private bool _isMoving;
    //Check Reset
    [SerializeField]private bool _isDead;
    public bool isDead => _isDead;
    
    //Check For Animator
    private bool _isBack = false;
    
    //Animation
    private float _animHorizontal, _animVertical;
    private float timeCountForBroken = 0;
    private float timeTriggerForBroken = 15f;
    
    
    
    //Sound
    private bool isPlaySound;
    private float soundDuration = 0.5f;
    
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

        _input.Player.InteractHold.started += OnInteractHoldStart;
        _input.Player.InteractHold.performed += OnInteractHoldPerformed;
        _input.Player.InteractHold.canceled += OnInteractHoldCanceled;

        _input.Player.GrabItem.started += OnGrabItem;
        _input.Player.GrabItem.canceled += OnGrabItem;
        
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 0);
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    
    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        if (_isMoving && !isPlaySound)
        {
            StartCoroutine(PlaySoundCoroutine(SoundManager.SoundName.FootStep));
        }

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
            
            _isMoving = true;
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

            _isMoving = true;

        }
        else if (_moveVector2.y > 0)
        {
            // Back
            _moveSpeed = 4.12f;
            _handDirection = (int) DirectionPlayer.North;
            
            _isMoving = true;

        }
        else if (_moveVector2.y < 0)
        {
            // Front
            _moveSpeed = 4.12f;
            _handDirection = (int) DirectionPlayer.South;

            _isMoving = true;
        }


        if (handFreeze == (int)DirectionPlayer.West)
        {
            if (_moveVector2.x > 0)
            {
                _animator.SetFloat("Vertical", 1);
            }
            else if (_moveVector2.x < 0)
            {
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Horizontal", 1);
            }
        }
        
        if (handFreeze == (int)DirectionPlayer.East)
        {
            if (_moveVector2.x < 0)
            {
                _animator.SetFloat("Vertical", 1);
            }
            else if (_moveVector2.x > 0)
            {
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Horizontal", 1);
            }
        }
        
        if (!isFreezeHand)
        {
            handFreeze = _handDirection;
            _hand.SentDirection(handFreeze);

            CheckHandFreezeForAnimation();
        }

    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        // idle
        _moveVector2 = Vector2.zero;
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 0);
        
        _isMoving = false;

        
        if (isGrabItem)
        {
            if (handFreeze == (int)DirectionPlayer.East)
            {
                _animator.SetFloat("Horizontal",0.3f);
            }
            if (handFreeze == (int)DirectionPlayer.West)
            {
                _animator.SetFloat("Horizontal",0.3f);
            }

            if (handFreeze == (int)DirectionPlayer.North)
            {
                _animator.SetFloat("Vertical",0.3f);
            }
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

        if (!_isHoldInteract)
        {
            _animator.SetBool("IsHoldDrag",false);
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

    public void CheckHandFreezeForAnimation()
    {
        switch (handFreeze)
        {
            case (int)DirectionPlayer.East:
                _animator.SetFloat("Horizontal",-1);
                _animator.SetFloat("Vertical", 0);
              
                break;
            case (int)DirectionPlayer.West:
                _animator.SetFloat("Horizontal",1);
                _animator.SetFloat("Vertical",0);
                break;
            case (int)DirectionPlayer.North:
                _animator.SetFloat("Horizontal",0);
                _animator.SetFloat("Vertical",1);
                break;
            case (int)DirectionPlayer.South:
                _animator.SetFloat("Horizontal",0);
                _animator.SetFloat("Vertical",-1);
                break;
            default:
                _animator.SetFloat("Horizontal",0);
                _animator.SetFloat("Vertical",0);
                break;

        }
    }
    
    public IEnumerator CheckDurationAnimation(string nameAnim,float duration)
    {
        _isMoving = false;
        _animator.SetBool(nameAnim,true); 
        Debug.Log("First");
        yield return new WaitForSeconds(duration);
        Debug.Log("Second");
        _isMoving = true;
    }
    
    IEnumerator PlaySoundCoroutine(SoundManager.SoundName soundName)
    {
        isPlaySound = true;
        SoundManager.instance.Play(soundName);

        yield return new WaitForSeconds(soundDuration);

        isPlaySound = false;
    }
    
    public void Crouching(GameObject player, bool isCrouching)
    {
        if (_isCanCrouching)
        {
            _item._isCanHold = !isCrouching;
            _item.PlaceItem();
            CapsuleCollider capsuleCollider = player.GetComponent<CapsuleCollider>();

            if (isCrouching)
            {
                // ++Animation crouching
                capsuleCollider.height = 1;
                capsuleCollider.center = new Vector3(0,0.5f,0.55f);
                
                _animator.SetBool("OnCrouch",true);
            }
            else
            {
                // --Animation crouching
                capsuleCollider.height = 2;
                capsuleCollider.center = new Vector3(0,1,0.55f);
                
                _animator.SetBool("OnCrouch",false);
            }
        }
    }

    

  
}

public enum  AnimName
{
    Horizontal,
    Vertical,
    IsHoldDrag,
    IsGrabItem,
    OnCrouch
}
