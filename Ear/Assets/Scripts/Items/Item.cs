using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform _handPoint;
    [SerializeField] private Transform _itemPoint;
    [SerializeField] private float _itemPointRadius = 0.5f;
    [SerializeField] private LayerMask _itemMask;

    private PlayerController _playerController;
    private GameObject _itemGameObject;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
    }

    public void HoldItem()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_itemPoint.position, 
            _itemPointRadius, _colliders, _itemMask);

        if(_numFound > 0)
        {
            // Test
            _itemGameObject = _colliders[0].gameObject;
            Debug.Log($"_itemGameObject = {_itemGameObject.name}");

            if (_itemGameObject != null)
            {
                Debug.Log("Hold Item");
                _itemGameObject.transform.SetParent(_handPoint);
                _itemGameObject.transform.localPosition = Vector3.zero;
                _itemGameObject.GetComponent<Collider>().enabled = false;
                _itemGameObject.GetComponent<Rigidbody>().useGravity = false;
                _itemGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                
                _playerController.isGrabItem = true;
            }

            /*if(interactable != null)
            {
                interactable.Interact(this);
            }*/
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
