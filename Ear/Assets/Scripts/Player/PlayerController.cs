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
    public Vector2 MoveVector2 => _moveVector2;
    
    private Interactor _interactor;
    private Item _item;
    private Hand _hand;
    private Rigidbody _rb;
    private InputSystems _input;
    private Animator _animator;

    [Header("Status Player")]
    public PlayerState _playerState = PlayerState.Idle;
    
    private bool _isHoldInteract = false;
    public bool _isHoldGrabItem = false;
    public bool isGrabItem = false;
    public bool _isCanCrouching = false;
    
    private int _handDirection;
    public int handDirection => _handDirection;
    public int handFreeze;
    public bool isFreezeHand = false;
    
    public Transform handLeft;
    public Transform handRight;
    public Vector3 handLeftPos;
    public Vector3 handRightPos;

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
        handRightPos = handRight.localPosition;
        handLeftPos = handLeft.localPosition;
        
        
        _input = new InputSystems();
        _rb = GetComponent<Rigidbody>();
        _interactor = GetComponent<Interactor>();
        _item = GetComponent<Item>();
        _hand = GetComponent<Hand>();
        _animator = GetComponentInChildren<Animator>();
        
        // idle
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 0);
    }
    
    private void Start()
    {
        GameManager.instance.ImPlayer(this.gameObject);
    }
    
    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;

        _input.Player.InteractHold.started += OnInteractHoldStart;
        _input.Player.InteractHold.performed += OnInteractHoldPerformed;
        _input.Player.InteractHold.canceled += OnInteractHoldCanceled;

        _input.Player.HoldGrabItem.started += OnHoldGrabItemStart;
        _input.Player.HoldGrabItem.performed += OnHoldGrabItemPerformed;
        _input.Player.HoldGrabItem.canceled += OnHoldGrabItemCanceled;
        //GameManager.instance.ImPlayer(this.gameObject);
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Movement.performed -= OnMovementPerformed;
        _input.Player.Movement.canceled -= OnMovementCanceled;

        _input.Player.InteractHold.started -= OnInteractHoldStart;
        _input.Player.InteractHold.performed -= OnInteractHoldPerformed;
        _input.Player.InteractHold.canceled -= OnInteractHoldCanceled;

        _input.Player.HoldGrabItem.started -= OnHoldGrabItemStart;
        _input.Player.HoldGrabItem.performed -= OnHoldGrabItemPerformed;
        _input.Player.HoldGrabItem.canceled -= OnHoldGrabItemCanceled;
    }

    private void Update()
    {
        if (_isMoving && !isPlaySound)
        {
            StartCoroutine(PlaySoundCoroutine(SoundManager.SoundName.FootStep));
        }
        switch (_playerState)
        {
            case PlayerState.Idle:
                _isDead = false;

                break;
            case PlayerState.Dead:
                _isDead = true;
                break;
            case PlayerState.Crouch:
                break;
            case PlayerState.HoldItem:
                _isHoldGrabItem = true;
                break;
            case PlayerState.DragObject:
                _isHoldInteract = true;
                break;
            default:
                _isDead = false;
                _isHoldInteract = false;
                _isHoldGrabItem = false;
                break;
        }

        if(transform.position.y < -25)
        {
            PlayerDEAD();
        }

    }

    private void FixedUpdate()
    {
        if (_playerState == PlayerState.Idle)
        {
            _rb.velocity = new Vector3(_moveVector2.x * _moveSpeed ,
                        _rb.velocity.y, _moveVector2.y * _moveSpeed * 0.75f);
        }
        else if (_playerState == PlayerState.DragObject || _playerState == PlayerState.Crouch)
        {
            _rb.velocity = new Vector3(_moveVector2.x * _moveSpeed * 0.75f,
                        _rb.velocity.y, _moveVector2.y * _moveSpeed * 0.5f);
        }
        else if (_playerState == PlayerState.Dead)
        {
            _rb.velocity = Vector3.zero;
        }


    }
    
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        // walk
        _moveVector2 = value.ReadValue<Vector2>();
        // _animator.SetTrigger("Walking");
        
        if (_moveVector2.x < 0)
        {
            // Left
            //_moveSpeed = 5.5f;
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
            //_moveSpeed = 5.5f;
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
            //_moveSpeed = 4.12f;
            _handDirection = (int) DirectionPlayer.North;
            
            _isMoving = true;

        }
        else if (_moveVector2.y < 0)
        {
            // Front
            //_moveSpeed = 4.12f;
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
    
    private void OnHoldGrabItemStart(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            //Debug.LogWarning("IT's JUST PRESS BY HOLD BUTTON");
        }

    }

    private void OnHoldGrabItemPerformed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if(context.interaction is HoldInteraction)
            {
                _item.HoldInteract();
                Debug.LogWarning("IT's REALLY HOLD");
                _isHoldGrabItem = true;
            }  
        }
        
    }


    private void OnHoldGrabItemCanceled(InputAction.CallbackContext context)
    {
        if (context.interaction is PressInteraction) // Just Press No Hold Interact!
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
            
            return;
        }
        if (_isHoldGrabItem)
        {
            if (!context.ReadValueAsButton()) Debug.Log("Released");
            _item.ReleasedHoldInteract();
            _isHoldGrabItem = false;
            
        }

        if (!_isHoldGrabItem)
        {
            _animator.SetBool("IsHoldDrag",false);
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
    
    public IEnumerator CheckDurationAnimation(string nameAnim,float duration,bool isPlayAnimation)
    {
        _moveVector2 = Vector2.zero;
        _input.Player.Movement.performed -= OnMovementPerformed;
        _animator.SetBool(nameAnim, isPlayAnimation);
        Debug.Log("First");
        yield return new WaitForSeconds(duration);
        Debug.Log("Second");
        _input.Player.Movement.performed += OnMovementPerformed;
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

                _playerState = PlayerState.Crouch;
                StartCoroutine(CheckDurationAnimation("OnCrouch", 1,true));
            }
            else
            {
                // --Animation crouching
                capsuleCollider.height = 2;
                capsuleCollider.center = new Vector3(0,1,0.55f);
                
                _playerState = PlayerState.Idle;
                StartCoroutine(CheckDurationAnimation("OnCrouch", 1,false));
            }
        }
    }

    public void CutScene_ReviveStartGame()
    {
        _animator.SetTrigger("OnlyStart");
        _input.Disable();
        StartCoroutine(CutScene_WaitFor7Sec());
    }

    public IEnumerator CutScene_WaitFor7Sec()
    {
        yield return new WaitForSeconds(8f);
        _input.Enable();
    }

    public void PlayerStandStill()
    {
        _moveVector2 = Vector2.zero;
        _rb.velocity = Vector3.zero;
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 0);
        _input.Player.Movement.performed -= OnMovementPerformed;
        _input.Player.Movement.canceled -= OnMovementCanceled;
    }

    public void PlayerCanWalkNow()
    {
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
    }

    public void PlayerDEAD()
    {
        StartCoroutine(RespawnTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "DeadZone":
                _animator.SetTrigger("Dead_OnWater");
                StartCoroutine(RespawnTime(1.5f));
                break;
            case "Acid":
                StartCoroutine(RespawnTime());
                break;
            case "Void":
                StartCoroutine(RespawnTime());
                break;
        }
    }


    IEnumerator RespawnTime()
    {
        _playerState = PlayerState.Dead;
        _input.Disable();

        yield return new WaitForSeconds(1f);
        transform.position = GameManager.instance.GiveMePositionReSpawn().position;
        GameManager.instance.ReloadScene();

        _playerState = PlayerState.Idle;
        _input.Enable();
    }

    IEnumerator RespawnTime(float timeDelay)
    {
        _playerState = PlayerState.Dead;
        _input.Disable();

        yield return new WaitForSeconds(timeDelay);
        transform.position = GameManager.instance.GiveMePositionReSpawn().position;
        GameManager.instance.ReloadScene();

        _playerState = PlayerState.Idle;
        _input.Enable();
    }
}

public enum PlayerState
{
    Idle,
    Dead,
    HoldItem,
    DragObject,
    Crouch,
}

