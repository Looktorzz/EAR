using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;
    [SerializeField] private LayerMask _itemMask;
    private Collider _collider = new Collider();
    private PlayerController _playerController;
    private Animator _animator;
    private Hand _hand;
    
    public GameObject itemInHand;
    public bool _isCanHold;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponentInChildren<Animator>();
        _hand = GetComponent<Hand>();
        _isCanHold = true;
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
                
                Debug.Log("Hold Item");
                itemInHand.transform.SetParent(null);
                itemInHand.transform.SetParent(_handTransform);
                itemInHand.transform.localPosition = Vector3.zero;
                itemInHand.GetComponent<Collider>().isTrigger = true;
                itemInHand.GetComponent<Rigidbody>().useGravity = false;
                itemInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                _animator.SetBool("IsGrabItem",true);
                
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

            if (itemInHand != null)
            {
                // ++Sound Place Item
                // ++Animation Place Item
                Debug.Log("Place Item");
                itemInHand.transform.SetParent(null);
                itemInHand.GetComponent<Collider>().isTrigger = false;
                itemInHand.GetComponent<Rigidbody>().useGravity = true;
                itemInHand.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;

                _collider = null;
                itemInHand = null;
                
                _playerController.CheckHandFreezeForAnimation();
                _animator.SetBool("IsGrabItem",false);
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
            // ++Sound install something
            
            // ++Animation install something
            Debug.Log("Place Item On Interact");
            itemInHand.transform.SetParent(null);
            itemInHand.GetComponent<Collider>().enabled = true;
            
            _collider = null;
            itemInHand = null;
            

            _playerController.isGrabItem = false;
        }
    }
}
