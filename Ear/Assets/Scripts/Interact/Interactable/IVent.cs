using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVent : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private bool isInteract = false;
    
    public bool Interact(Interactor interactor)
    {
        if (interactor != null)
        {
            isInteract = true;
            Crouching(interactor.gameObject, true);
            return true;
        }

        return false;
    }

    private void Crouching(GameObject player, bool isCrouching)
    {
        if (isInteract)
        {
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
            Crouching(player.gameObject, true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactor player))
        {
            Crouching(player.gameObject, false);
            isInteract = false;
        }
    }
}
