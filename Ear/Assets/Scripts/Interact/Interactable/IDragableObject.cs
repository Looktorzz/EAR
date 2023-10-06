using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IHoldInteractable
{
    bool IsDragNow = false;
    Rigidbody rb;

    [SerializeField] private bool isBasin;
    [SerializeField] private bool isBox;
    
    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    public bool HoldInteract(Interactor interactor)
    {
        GameObject player = interactor.gameObject;
        
        if (!IsDragNow)
        {
            // ++Animation drag
            player.GetComponent<Item>().PlaceItem();
            player.GetComponent<PlayerController>().isFreezeHand = true;
            
            IsDragNow = true;
            transform.parent = player.transform;
            Debug.Log("Check Now Drag");
            // rb.isKinematic = true;
            return true;
        }

        if (isBasin)
        {
            SoundManager.instance.Play(SoundManager.SoundName.DragBucket);
        }

        Debug.LogWarning("They are No Hold Interact");
        return false;
    }

    public void HoldCompleteInteract()
    {
        throw new System.NotImplementedException();
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        GameObject player = interactor.gameObject;
        
        if(IsDragNow)
        {
            // --Animation drag
            player.GetComponent<PlayerController>().isFreezeHand = false;
            
            IsDragNow = false;
            transform.parent = null;
            Debug.Log("Check Now Not Drag");
            // rb.isKinematic = false;
            return true;
        }
        return false;
    }
}
