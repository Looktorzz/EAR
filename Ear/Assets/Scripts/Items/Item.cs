using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;
    [SerializeField] private LayerMask _itemMask;
    private Collider _collider = new Collider();
    private Hand _hand;

    private PlayerController _playerController;
    private GameObject _itemInHand;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _hand = GetComponent<Hand>();
    }

    public void HoldItem()
    {
        _collider = _hand.SentColliderFound(_itemMask);

        if(_collider != null)
        {
            _itemInHand = _collider.gameObject;

            if (_itemInHand != null)
            {
                Debug.Log("Hold Item");
                _itemInHand.transform.SetParent(_handTransform);
                _itemInHand.transform.localPosition = Vector3.zero;
                _itemInHand.GetComponent<Collider>().enabled = false;
                _itemInHand.GetComponent<Rigidbody>().useGravity = false;
                _itemInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                
                _playerController.isGrabItem = true;
            }
        }
    }

    public void PlaceItem()
    {
        if (_itemInHand != null)
        {
            Debug.Log("Place Item");
            _itemInHand.transform.SetParent(null);
            _itemInHand.GetComponent<Collider>().enabled = true;
            _itemInHand.GetComponent<Rigidbody>().useGravity = true;
            _itemInHand.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
            
            _collider = null;
            _itemInHand = null;
            _playerController.isGrabItem = false;
        }
    }
}
