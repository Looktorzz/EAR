using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;
    [SerializeField] private LayerMask _itemMask;
    [SerializeField] private LayerMask _DragItemMask;
    private Collider _collider = new Collider();
    private Collider _colliderDrag = new Collider();
    private PlayerController _playerController;
    private Animator _animator;
    private Hand _hand;
    private int handPos;
    
    public GameObject itemInHand;
    public bool _isCanHold;
    public bool isInteractFromSpace;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponentInChildren<Animator>();
        _hand = GetComponent<Hand>();
        _isCanHold = true;
        isInteractFromSpace = false;

        handPos = -1;
    }

    private void FixedUpdate() 
    {
        if (itemInHand != null)
        {
            if (_playerController.handFreeze != handPos)
            {
                SpriteRenderer spriteRenderer = itemInHand.GetComponentInChildren<SpriteRenderer>();
                handPos = _playerController.handFreeze;
                switch (_playerController.handFreeze)
                {
                    case (int)DirectionPlayer.East:
                    case (int)DirectionPlayer.North:
                    case (int)DirectionPlayer.South:
                        spriteRenderer.flipX = false;
                        break;
                    case (int)DirectionPlayer.West:
                        spriteRenderer.flipX = true;
                        break;
                }
                //Debug.Log("test 1");
            }
        }
        else
        {
            if (handPos > 0)
            {
                handPos = -1;
                //Debug.Log("test 2");
            }
        }
    }

    public void HoldItem()
    {
        _collider = _hand.SentColliderFound(_itemMask);

        if(_collider != null && _isCanHold)
        {
            itemInHand = _collider.gameObject;

            if (itemInHand != null)
            {
                // ++Sound Hold/Grab Item
                
                // ++Animation Hold/Grab Item
                
                //Debug.Log("Hold Item");
                itemInHand.transform.SetParent(null);
                itemInHand.transform.SetParent(_handTransform);
                itemInHand.transform.localPosition = new Vector3(0,0,-0.3f);
                itemInHand.GetComponent<Collider>().isTrigger = true;
                itemInHand.GetComponent<Rigidbody>().useGravity = false;
                itemInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                
                //_animator.SetBool("IsGrabItem",true);
                StartCoroutine(_playerController.GrabAnimationDuration(0.1f,true));

                

                _playerController.isGrabItem = true;
            }
            
            _hand.ClearCollider();
        }
    }

    

    public void PlaceItem()
    {
        if(_collider != null)
        {
            itemInHand = _collider.gameObject;
            itemInHand.GetComponentInChildren<SpriteRenderer>().flipX = false;

            if (_collider.gameObject.GetComponent<Fuse>() || 
                _collider.gameObject.GetComponent<Lever>())
            {
                isInteractFromSpace = true;
                GetComponent<Interactor>().PressInteract();
                isInteractFromSpace = false;
                // return;
            }
            
            if (itemInHand != null)
            {
                // ++Sound Place Item
                // ++Animation Place Item
                //Debug.Log("Place Item");
                itemInHand.transform.SetParent(null);
                itemInHand.GetComponent<Collider>().isTrigger = false;
                itemInHand.GetComponent<Rigidbody>().useGravity = true;

                //Need to Fix : Make it only Item  in collider , item that can drag NO NEED TO BE HERE!!
                itemInHand.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
                
                _collider = null;
                itemInHand = null;
                
                _playerController.CheckHandFreezeForAnimation();
                
                _animator.SetBool("IsGrabItem",false);
                // StartCoroutine(_playerController.CheckDurationAnimation("IsGrabItem", 0.5f));
                _animator.SetFloat("Horizontal", 0);
                _animator.SetFloat("Vertical", 0);

                
                _playerController.isGrabItem = false;
            }
            
            _hand.ClearCollider();
        }
    }

    public void PlaceItemOnInteract()
    {
        if (itemInHand != null)
        {
            itemInHand.GetComponentInChildren<SpriteRenderer>().flipX = false;
            
            // ++Sound install something
            SoundManager.instance.Play(SoundManager.SoundName.Insert);
            
            // ++Animation install something
            //Debug.Log("Place Item On Interact");
            itemInHand.transform.SetParent(null);
            itemInHand.GetComponent<Collider>().enabled = true;
            
            _collider = null;
            itemInHand = null;

            _playerController.CheckHandFreezeForAnimation();
            
            _animator.SetBool("IsGrabItem",false);
            // StartCoroutine(_playerController.CheckDurationAnimation("IsGrabItem", 0.5f));
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 0);

            _playerController.isGrabItem = false;
        }
    }

    public void PlaceItemOnInteractOnlyBin()
    {
        if (itemInHand != null)
        {
            itemInHand.GetComponentInChildren<SpriteRenderer>().flipX = false;

            // ++Sound install something
            //SoundManager.instance.Play(SoundManager.SoundName.Insert);

            // ++Animation install something
            //Debug.Log("Place Item On Interact");
            itemInHand.transform.SetParent(null);
            itemInHand.GetComponent<Collider>().enabled = true;

                itemInHand.GetComponent<Collider>().isTrigger = false;
                itemInHand.GetComponent<Rigidbody>().useGravity = true;

                itemInHand.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
            


            _collider = null;
            itemInHand = null;

            _playerController.CheckHandFreezeForAnimation();

            _animator.SetBool("IsGrabItem", false);
            // StartCoroutine(_playerController.CheckDurationAnimation("IsGrabItem", 0.5f));
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 0);

            _playerController.isGrabItem = false;
        }
    }

    public void HoldInteract()
    {
        _colliderDrag = _hand.SentColliderFound(_DragItemMask);

        if(_colliderDrag != null)
        {
            IHoldGrabItem item = _colliderDrag.GetComponent<IHoldGrabItem>();

            if (item != null)
            {
                // ++Animation Drag Item
                // _animator.SetBool("IsHoldDrag",true);
                StartCoroutine(_playerController.CheckDurationAnimation("IsHoldDrag", .5f,true));
                _animator.SetFloat("Horizontal", 0);
                _animator.SetFloat("Vertical", 0);
                
                item.HoldInteract(this);
            }
            
            _hand.ClearCollider();
        }
    }

    public void ReleasedHoldInteract()
    {
        _colliderDrag = _hand.SentColliderFound(_DragItemMask);

        if(_colliderDrag != null)
        {
            IHoldGrabItem item = _colliderDrag.GetComponent<IHoldGrabItem>();

            if (item != null)
            {
                // --Animation released hold interact
                
                item.ReleasedInteract(this);
            }

            StartCoroutine(_playerController.CheckDurationAnimation("IsHoldDrag", .25f, false));
            _hand.ClearCollider();
        }
    }
}
