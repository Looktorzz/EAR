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
    
    private Collider[] _colliders = new Collider[3];
    
    void Update()
    {
        _handPoint.position = _handDirectionPoint[_index].position;
    }

    public void SentDirection(int index)
    {
        _index = index;
    }

    public Collider SentColliderFound(LayerMask layerMask)
    {
        Physics.OverlapSphereNonAlloc(_handPoint.position, 
            _handRadius, _colliders, layerMask);
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
