using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVent : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private GameObject _player;
    private Item _item;
    private bool _isPlayerInArea = false;

    private PlayerController _playerController;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
        
        if (_player != null)
        {
            _item = _player.GetComponent<Item>();
            _playerController = _player.GetComponent<PlayerController>();
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (interactor != null && _isPlayerInArea)
        {
            _playerController._isCanCrouching = true;
            _playerController.Crouching(interactor.gameObject, true);
            return true;
        }
        
        return false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactor player))
        {
            _isPlayerInArea = true;
            _playerController.Crouching(player.gameObject, true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactor player))
        {
            _playerController.Crouching(player.gameObject, false);
            _isPlayerInArea = false;
            _playerController._isCanCrouching = false;
        }
    }
}
