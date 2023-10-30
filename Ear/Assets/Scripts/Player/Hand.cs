using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform[] _handDirectionPoint;
    [SerializeField] private Transform _handPoint;
    [SerializeField] private float _handRadius = 0.4f;
    private int _index = 0;

    [SerializeField] private Material _materialOutline;
    [SerializeField] private Material _materialDefault;
    [SerializeField] private LayerMask _layerMask;
    private GameObject _gameObject;
    private GameObject _gameObject2;


    private Collider[] _colliders = new Collider[3];
    private Collider[] _colliderForOutLine = new Collider[3];

    private IInteractable _interactable;
    [SerializeField] int _numFound;
    bool IsfirstTime =  true;

    [SerializeField] private GameObject _handSize;
    [SerializeField] private GameObject _handSizeRL;
    [SerializeField] private GameObject _handSizeFB;
    [SerializeField] private PlayerController _playerController;

    void Update()
    {
        _handPoint.position = _handDirectionPoint[_index].position;
        // ==============================

        _numFound = Physics.OverlapBoxNonAlloc(_handPoint.position, _handSize.transform.localScale / 2,
            _colliderForOutLine, Quaternion.identity, _layerMask);
        
        if (_numFound > 0)
        {
            _gameObject = _colliderForOutLine[0].gameObject;
        }
        else
        {
            _gameObject = null;
            if (!IsfirstTime)
            {
                if (_gameObject2.GetComponentInChildren<SpriteRenderer>())
                {
                    _gameObject2.GetComponentInChildren<SpriteRenderer>().material = _materialDefault;
                }
                
            }
            
        }
        
        if (_gameObject != null)
        {
            if (IsfirstTime)
            {
                _gameObject2 = _gameObject; 
                IsfirstTime = false;
            } 
            if (_gameObject != _gameObject2)
            {
                _gameObject2.GetComponentInChildren<SpriteRenderer>().material = _materialDefault;
                _gameObject2 = _gameObject;
                return;
            }

            _gameObject2.GetComponentInChildren<SpriteRenderer>().material = _materialOutline;

        }

    }

    public void SentDirection(int index)
    {
        _index = index;
    }

    public Collider SentColliderFound(LayerMask layerMask)
    {
        if (_playerController.handFreeze == (int) DirectionPlayer.North ||
            _playerController.handFreeze == (int) DirectionPlayer.South)
        {
            _handSize.transform.localScale = _handSizeFB.transform.localScale;
        }
        else
        {
            _handSize.transform.localScale = _handSizeRL.transform.localScale;
        }
        
        Physics.OverlapBoxNonAlloc(_handPoint.position, _handSize.transform.localScale / 2,
            _colliders, Quaternion.identity, layerMask);
        
        return _colliders[0];
    }

    public void ClearCollider()
    {
        Array.Clear(_colliders,0,_colliders.Length);
    }
    
    private void OnDrawGizmos()
    {
        if (_playerController.handFreeze == (int) DirectionPlayer.North ||
            _playerController.handFreeze == (int) DirectionPlayer.South)
        {
            _handSize.transform.localScale = _handSizeFB.transform.localScale;
        }
        else
        {
            _handSize.transform.localScale = _handSizeRL.transform.localScale;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_handPoint.position, _handSize.transform.localScale);
    }
}
