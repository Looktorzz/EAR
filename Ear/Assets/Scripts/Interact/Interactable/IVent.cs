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
    private bool _isInteract = false;
    private bool _isPlayerinArea = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _item = _player.GetComponent<Item>();
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (interactor != null && _isPlayerinArea)
        {
            _isInteract = true;
            Crouching(interactor.gameObject, true);
            return true;
        }

        return false;
    }

    private void Crouching(GameObject player, bool isCrouching)
    {
        if (_isInteract)
        {
            _item._isCanHold = !isCrouching;
            _item.PlaceItem();
            CapsuleCollider capsuleCollider = player.GetComponent<CapsuleCollider>();

            if (isCrouching)
            {
                capsuleCollider.height = 1;
                capsuleCollider.center = new Vector3(0,0.5f,0.55f);
            }
            else
            {
                capsuleCollider.height = 2;
                capsuleCollider.center = new Vector3(0,1,0.55f);
            }
        
            player.transform.GetChild(0).gameObject.SetActive(!isCrouching);
            player.transform.GetChild(1).gameObject.SetActive(isCrouching);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactor player))
        {
            _isPlayerinArea = true;
            Crouching(player.gameObject, true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactor player))
        {
            Crouching(player.gameObject, false);
            _isPlayerinArea = false;
            _isInteract = false;
        }
    }
}