using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    [SerializeField] private PlayerController _player;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    void Update()
    {
        // Delete when game complete
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
        _interactionPointRadius, _colliders, _interactableMask);
    }

    public void PressInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
            _interactionPointRadius, _colliders, _interactableMask);

        if(_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact(this);
            } 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

}
