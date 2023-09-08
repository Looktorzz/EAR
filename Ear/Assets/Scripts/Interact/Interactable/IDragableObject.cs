using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IInteractable , IHoldInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    bool IsDragNow = false;
    [SerializeField] private GameObject player;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public bool Interact(Interactor interactor)
    {    
        Debug.Log("Something Wrong!");
        return false;
    }

    public bool HoldInteract(Interactor interactor)
    {
        if (!IsDragNow)
        {
            IsDragNow = true;
            transform.parent = player.transform;
            Debug.Log("Check Now Drag");
            rb.isKinematic = true;
            return true;
        }

        Debug.LogWarning("They are No Hold Interact");
        return false;
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        if(IsDragNow)
        {
            IsDragNow = false;
            transform.parent = null;
            Debug.Log("Check Now Not Drag");
            rb.isKinematic = false;
            return true;
        }
        return false;
    }
}
