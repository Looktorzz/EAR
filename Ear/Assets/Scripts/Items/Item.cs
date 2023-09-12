using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform _handGameObject;
    [SerializeField] private LayerMask _itemMask;
    private Collider _collider = new Collider();
    private Hand _hand;

    private PlayerController _playerController;
    private GameObject _itemGameObject;

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
            _itemGameObject = _collider.gameObject;

            if (_itemGameObject != null)
            {
                Debug.Log("Hold Item");
                _itemGameObject.transform.SetParent(_handGameObject);
                _itemGameObject.transform.localPosition = Vector3.zero;
                _itemGameObject.GetComponent<Collider>().enabled = false;
                _itemGameObject.GetComponent<Rigidbody>().useGravity = false;
                _itemGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                
                _playerController.isGrabItem = true;
            }
        }
    }

    public void PlaceItem()
    {
        if (_itemGameObject != null)
        {
            Debug.Log("Place Item");
            _itemGameObject.transform.SetParent(null);
            _itemGameObject.GetComponent<Collider>().enabled = true;
            _itemGameObject.GetComponent<Rigidbody>().useGravity = true;
            _itemGameObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
            
            _playerController.isGrabItem = false;
        }
    }
}
