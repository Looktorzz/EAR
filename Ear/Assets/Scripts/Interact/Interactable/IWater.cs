using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWater : MonoBehaviour , IHoldInteractable , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private bool isAcidWater;
    private CountdownTime _countdown;
    private Bucket _bucket;

    public bool Interact(Interactor interactor)
    {
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket.isFull)
        {
            if (bucket.isAcidWater == isAcidWater)
            {
                bucket.isFull = false;
                return true;
            }
            else
            {
                Debug.Log("Can't fill this water");
            }
        }

        return false;
    }

    public bool HoldInteract(Interactor interactor)
    {
        _bucket = interactor.GetComponentInChildren<Bucket>();
        _countdown = interactor.GetComponent<CountdownTime>();

        if (_bucket == null)
        {
            Debug.Log("Don't have Bucket");
            return false;
        }

        if (!_bucket.isFull)
        {
            _countdown.Countdown(true);
            return true;
        }

        return false;
    }
    
    public void HoldCompleteInteract()
    {
        _bucket.isAcidWater = isAcidWater;
        _bucket.isFull = true;
        Debug.Log("Hello");
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        _countdown = interactor.GetComponent<CountdownTime>();
        _countdown.Countdown(false);
        return false;
    }
}
