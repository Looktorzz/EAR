using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IBaseLever : MonoBehaviour , IHoldInteractable , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private Transform _leverPosition;
    [SerializeField] private GameObject _doorLever;
    [SerializeField] private bool _isCanPick;
    private CountdownTime _countdown;
    private Lever _lever;
    
    public bool Interact(Interactor interactor)
    {
        Lever lever = interactor.GetComponentInChildren<Lever>();
        
        if (lever != null)
        {
            interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
            lever.transform.SetParent(_leverPosition);
            lever.transform.localPosition = Vector3.zero;
            lever.isOnBaseLever = true;
            
            if (!_isCanPick)
            {
                lever.gameObject.layer = 0; 
            }
            
            return true;
        }

        // ++Sound fail (pak pak)
        SoundManager.instance.Play(SoundManager.SoundName.Fail);
        Debug.Log("You don't have Lever");
        return false;
    }
    
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
}
