using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IFuseBox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private Transform _fusePosition;
    [SerializeField] private GameObject _closeLock;
    [SerializeField] private GameObject _close;
    [SerializeField] private GameObject _open;
    [SerializeField] private GameObject _fuseItem;
    [SerializeField] private bool _isHaveFuseInside;
    [SerializeField] private bool _isUseKeyToOpen;
    
    [HideInInspector] public bool isHaveFuse = false;
    private bool _isOpen = false;
    
    [Header("Light")]
    [SerializeField] private GameObject _lightRed;
    [SerializeField] private GameObject _lightGreen;

    private void Start()
    {
        if (_isUseKeyToOpen)
        {
            _closeLock.SetActive(true);
            _close.SetActive(false);
        }
        else
        {
            _close.SetActive(!_isOpen);
            _open.SetActive(_isOpen);
        }
        
        _lightRed.SetActive(!isHaveFuse);
        _lightGreen.SetActive(isHaveFuse);

        
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
            }
            
            // ++Sound open fuse box
            SoundManager.instance.Play(SoundManager.SoundName.FuseBoxOn);
            
            _isOpen = true;
            _closeLock.SetActive(!_isOpen);
            _close.SetActive(!_isOpen);
            _open.SetActive(_isOpen);
            
            if (_isHaveFuseInside)
            {
                // ++Sound item drop
                
                
                _fuseItem.SetActive(true);
            }
            
            return true;
        }
        
        Fuse fuse = interactor.GetComponentInChildren<Fuse>();
        Debug.Log("Test Click Lever");

        if (fuse != null)
        {
            interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
            fuse.transform.SetParent(_fusePosition);
            fuse.transform.localPosition = Vector3.zero;
            fuse.gameObject.layer = 0;
            isHaveFuse = true;
            
            _lightRed.SetActive(!isHaveFuse);
            _lightGreen.SetActive(isHaveFuse);
            Debug.Log("Complete Fill Fuse.");
            
            SoundManager.instance.Play(SoundManager.SoundName.InsertFuse);

            return true;
        }

        // ++Sound fail (pak pak)
        return false;
    }
}
