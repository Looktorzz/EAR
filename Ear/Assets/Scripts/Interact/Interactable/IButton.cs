using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private DoorCanPause _door;


    public bool Interact(Interactor interactor)
    {
        if (_door != null)
        {
            // Freeze
            if (_door.IsDoorOpen)
            {
                _door.PauseDoor();
                return true;
            }
            return false;
        }
        else
        {
            // Down
            // _door.transform.DOMoveY()
        }

        return false;
    }
}
