using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IHoldGrabItem
{
    bool IsDragNow = false;
    Rigidbody rb;
    PlayerController playerController;

    [SerializeField] float PowerForce = 5000f;
    [SerializeField] private bool isBasin;
    [SerializeField] private bool isBox;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (IsDragNow)
        {
            Debug.Log(playerController.MoveVector2);
            if (playerController.MoveVector2.x > 0)
            {
                rb.AddForce(PowerForce * playerController.MoveVector2, ForceMode.Force);
            }
            else if (playerController.MoveVector2.y > 0)
            {
                rb.AddForce(PowerForce * playerController.MoveVector2, ForceMode.Force);
            }
        }
        else
        {

        }
    }

    public bool HoldInteract(Item item)
    {
        GameObject player = item.gameObject;
        
        if (!IsDragNow)
        {
            // ++Animation drag
            player.GetComponent<Item>().PlaceItem();
            player.GetComponent<PlayerController>().isFreezeHand = true;
            
            IsDragNow = true;
            
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

    public bool ReleasedInteract(Item item)
    {
        GameObject player = item.gameObject;
        
        if(IsDragNow)
        {
            // --Animation drag
            player.GetComponent<PlayerController>().isFreezeHand = false;
            
            IsDragNow = false;
            
            Debug.Log("Check Now Not Drag");
            // rb.isKinematic = false;
            return true;
        }
        return false;
    }
}
