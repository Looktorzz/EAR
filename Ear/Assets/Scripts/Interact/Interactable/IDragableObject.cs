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
            PlayerController pc = player.GetComponent<PlayerController>();
            Hand hand = player.GetComponent<Hand>();
            pc.isFreezeHand = true;
            
            
            IsDragNow = true;

            Collider collider = this.gameObject.GetComponent<Collider>();
            
            if ((int)DirectionPlayer.East == pc.handFreeze)
            {
                Vector3 moveDirection = pc.handLeft.position - this.transform.position;
                //300f force
                rb.AddForce(moveDirection * 300f);
                
                if (collider != null)
                {
                    SetHandInCenterObject(collider,pc.handLeft);
                }
            }

            if ((int) DirectionPlayer.West == pc.handFreeze)
            {
                Vector3 moveDirection = pc.handRight.position - this.transform.position;
                rb.AddForce(moveDirection * 300f);
                
                if (collider != null)
                {
                    SetHandInCenterObject(collider,pc.handRight);
                }
            }
        
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; 
            
            
            Debug.Log("Check Now Drag");
            // rb.isKinematic = true;
            return true;
        }

        Debug.LogWarning("They are No Hold Interact");
        return false;
    }

    public void SetHandInCenterObject(Collider collider, Transform handPoint)
    {
        Vector3 centerBottom = collider.bounds.center;
        centerBottom.y = collider.bounds.min.y;

        handPoint.position = centerBottom;
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
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.isFreezeHand = false;
            pc.handLeft.position = pc.handLeftPos;
            pc.handRight.position = pc.handRightPos;
            
            player.GetComponent<PlayerController>().isFreezeHand = false;
            
            
            
            IsDragNow = false;
            
            Debug.Log("Check Now Not Drag");
            // rb.isKinematic = false;
            return true;
        }
        return false;
    }
}
