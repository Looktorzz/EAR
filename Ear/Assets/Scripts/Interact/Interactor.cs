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
        _hand = GetComponent<Hand>();
    }

    public void PressInteract()
    {
        _collider = _hand.SentColliderFound(_interactableMask);

        if(_collider != null)
        {
            IInteractable interactable = _collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                // ++Animation interact something
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
                // ++Animation hold interact something
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
                // --Animation released hold interact
                interactable.ReleasedInteract(this);
            }
            
            _hand.ClearCollider();
        }
    }
}
