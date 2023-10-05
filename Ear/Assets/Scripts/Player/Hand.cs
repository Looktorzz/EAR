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

    /*[SerializeField] private Material _materialOutline;
    [SerializeField] private LayerMask _layerMask;
    private Material _materialDefault;
    private GameObject _gameObject;*/

    private Collider[] _colliders = new Collider[3];
    // public Collider[] _colliders = null;
    
    void Update()
    {
        _handPoint.position = _handDirectionPoint[_index].position;
        
        /*_gameObject = SentColliderFound(_layerMask).gameObject;
        if (_gameObject != null)
        {
            Debug.Log($"_gameObject.name = {_gameObject.name}");
            _materialDefault = _gameObject.GetComponentInChildren<SpriteRenderer>().material;
            _gameObject.GetComponentInChildren<SpriteRenderer>().material = _materialOutline;
        }*/

    }

    public void SentDirection(int index)
    {
        _index = index;
    }

    public Collider SentColliderFound(LayerMask layerMask)
    {
        Physics.OverlapSphereNonAlloc(_handPoint.position, 
            _handRadius, _colliders, layerMask);
        /*_colliders = Physics.OverlapSphere(_handPoint.position, 
            _handRadius, layerMask);*/
        
        return _colliders[0];
    }

    public void ClearCollider()
    {
        Array.Clear(_colliders,0,_colliders.Length);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_handPoint.position, _handRadius);
    }
}
