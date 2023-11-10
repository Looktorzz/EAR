using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IBaseLever : MonoBehaviour ,/* IHoldInteractable ,*/ IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private Transform _leverPosition;
    [SerializeField] private DoorLever _doorLever;
    [SerializeField] private bool _isCanPick;
    private CountdownTime _countdown;
    private Lever _lever;
    private bool _isOpen = false;
    
    public bool Interact(Interactor interactor)
    {
        Lever leverOnPlayer = interactor.GetComponentInChildren<Lever>();
        
        if (leverOnPlayer != null)
        {
            interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
            leverOnPlayer.transform.SetParent(_leverPosition);
            leverOnPlayer.transform.localPosition = Vector3.zero;
            leverOnPlayer.isOnBaseLever = true;
            
            if (!_isCanPick)
            {
                leverOnPlayer.gameObject.layer = 0; 
            }
            
            return true;
        }

        _lever = this.gameObject.GetComponentInChildren<Lever>();
        if (_lever != null && !_isOpen)
        {
            // ++Sound open door by interact lever
            _lever.SetLeverOpen(true);
            _doorLever.OpenDoor();
            _isOpen = true;
            
            return true;
        }

        // ++Sound fail (pak pak)
        SoundManager.instance.Play(SoundManager.SoundName.Fail);
        Debug.Log("You don't have Lever");
        return false;
    }
    
    /*
    public bool HoldInteract(Interactor interactor)
    {
        _lever = GetComponentInChildren<Lever>();
        _countdown = interactor.GetComponent<CountdownTime>();

        if (_lever != null)
        {
            // ++Sound hold interact Lever ( rotate Lever )
            _countdown.Countdown(true);
            return true;
        }

        // ++Sound fail (pak pak)
        SoundManager.instance.Play(SoundManager.SoundName.Fail);
        Debug.Log("Don't have Lever on BaseLever");
        return false;
    }

    public void HoldCompleteInteract()
    {
        // Open ( If have more 1 sound or anim / Add variable at upper c: )
        // ++Sound open door by interact lever
        _lever.SetLeverOpen(true);
        // _animator.SetTrigger("OpenByLever");
        Vector3 posDoor = _doorLever.transform.position;
        _doorLever.transform.DOLocalMoveY(posDoor.y + 1, 3).SetEase(Ease.OutBounce);
        
        SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        // --Stop Sound hold interact Lever ( rotate Lever )
        _countdown = interactor.GetComponent<CountdownTime>();
        _countdown.Countdown(false);
        return false;
    }
    */
}
