using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform[] _interactionDirectionPoint;
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private int _index = 0;

    void Update()
    {
        //_interactionPoint.position = _interactionDirectionPoint[_index].position;
        
        // Delete when game complete
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
        _interactionPointRadius, _colliders, _interactableMask);
    }

    public void SentDirection(int index)
    {
        _index = index;
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

    public void HoldInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position,
            _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IHoldInteractable>();

            if (interactable != null)
            {
                interactable.HoldInteract(this);
            }
        }
    }

    public void ReleasedHoldInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position,
            _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IHoldInteractable>();

            if (interactable != null)
            {
                interactable.ReleasedInteract(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

}
