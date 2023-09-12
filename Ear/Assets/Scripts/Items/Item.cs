using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform[] _itemDirectionPoint;
    [SerializeField] private Transform _itemPoint;
    [SerializeField] private float _itemPointRadius = 0.5f;
    [SerializeField] private LayerMask _itemMask;

    private PlayerController _playerController;
    private GameObject _itemGameObject;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;
    
    private int _index = 0;

    private void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        _itemPoint.position = _itemDirectionPoint[_index].position;
    }

    public void SentDirection(int index)
    {
        _index = index;
    }

    public void HoldItem()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_itemPoint.position, 
            _itemPointRadius, _colliders, _itemMask);

        if(_numFound > 0)
        {
            _itemGameObject = _colliders[0].gameObject;

            if (_itemGameObject != null)
            {
                Debug.Log("Hold Item");
                _itemGameObject.transform.SetParent(_itemPoint);
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
