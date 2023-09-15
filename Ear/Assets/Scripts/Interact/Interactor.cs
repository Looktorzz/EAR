using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableMask;
    private Collider _collider = new Collider();
    private Hand _hand;

    private void Start()
    {
<<<<<<< HEAD
        //_interactionPoint.position = _interactionDirectionPoint[_index].position;
        
        // Delete when game complete
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
        _interactionPointRadius, _colliders, _interactableMask);
    }

    public void SentDirection(int index)
    {
        _index = index;
=======
        _hand = GetComponent<Hand>();
>>>>>>> Movement
    }

    public void PressInteract()
    {
        _collider = _hand.SentColliderFound(_interactableMask);

        if(_collider != null)
        {
            IInteractable interactable = _collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact(this);
            }

            _hand.ClearCollider();
        }
    }

    public void HoldInteract()
    {
        _collider = _hand.SentColliderFound(_interactableMask);

        if(_collider != null)
        {
            IHoldInteractable interactable = _collider.GetComponent<IHoldInteractable>();

            if (interactable != null)
            {
                interactable.HoldInteract(this);
            }
            
            _hand.ClearCollider();
        }
    }

    public void ReleasedHoldInteract()
    {
        _collider = _hand.SentColliderFound(_interactableMask);

        if(_collider != null)
        {
            IHoldInteractable interactable = _collider.GetComponent<IHoldInteractable>();

            if (interactable != null)
            {
                interactable.ReleasedInteract(this);
            }
            
            _hand.ClearCollider();
        }
    }
}
