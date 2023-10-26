using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ILeverBox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _closeLock;
    [SerializeField] private GameObject _close;
    [SerializeField] private GameObject _openOff;
    [SerializeField] private GameObject _openOn;
    [SerializeField] private bool _isUseKeyToOpen;
    [SerializeField] private bool _isOpen;
    
    [SerializeField] private DoorLever _doorLever;
    
    [Header("Light")]
    [SerializeField] private GameObject _lightRed;
    [SerializeField] private GameObject _lightGreen;

    private void Start()
    {
        _closeLock.SetActive(_isUseKeyToOpen);
        _close.SetActive(!_isUseKeyToOpen);
        _openOff.SetActive(_isOpen);
        _openOn.SetActive(false);
        
        _lightRed.SetActive(!_isOpen);
        _lightGreen.SetActive(_isOpen);

    }

    public bool Interact(Interactor interactor)
    {
        if (!_isOpen)
        {
            if (_isUseKeyToOpen)
            {
                Keys key = interactor.GetComponentInChildren<Keys>();
                if (key == null)
                {
                    Debug.Log("Can't Open Fuse Box");
                    return false;
                }
                else
                {
                    interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
                    Destroy(key.gameObject);
                }
            }
            
            // ++Sound open fuse box
            SoundManager.instance.Play(SoundManager.SoundName.FuseBoxOn);
            
            _isOpen = true;
            _closeLock.SetActive(!_isOpen);
            _close.SetActive(!_isOpen);
            _openOff.SetActive(_isOpen);
        }
        else
        {
            _openOff.SetActive(false);
            _openOn.SetActive(true);
            
            _lightRed.SetActive(!_isOpen);
            _lightGreen.SetActive(_isOpen);
            
            _doorLever.OpenDoor();
            
            /*Vector3 posDoor = _doorLever.transform.position;
            _doorLever.transform.DOLocalMoveY(posDoor.y + 1, 3).SetEase(Ease.OutBounce);*/
        
            // SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
            
            return true;
        }
        
        return false;
    }
}
