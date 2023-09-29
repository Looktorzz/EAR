using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPicture : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _canva;
    private bool _isOpen = false;

    private void Start()
    {
        _canva.SetActive(false);
    }

    public bool Interact(Interactor interactor)
    {
        Rigidbody playerRb = interactor.GetComponent<Rigidbody>();
        PlayerController playerController = interactor.GetComponent<PlayerController>();
        
        if (_isOpen)
        {
            playerRb.constraints = RigidbodyConstraints.FreezeRotation;
            playerController.isFreezeHand = false;
            _isOpen = false;
            _canva.SetActive(_isOpen);
        }
        else
        {
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            playerController.isFreezeHand = true;
            _isOpen = true;
            _canva.SetActive(_isOpen);
        }
        
        return true;
    }
}
