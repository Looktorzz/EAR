using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IHoldGrabItem
{
    bool IsDragNow = false;
    Rigidbody rb;
    PlayerController pc;

    [SerializeField] float PowerForce = 5000f;
    [SerializeField] private bool isBasin;
    [SerializeField] private bool isBox;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        
    }

    private void FixedUpdate()
    {
        if (IsDragNow)
        {
            if ((int)DirectionPlayer.East == pc.handFreeze)
            {
                Vector3 moveDirection = pc.handLeft.position - this.transform.position;
                rb.AddForce(moveDirection * 300f);
            }

            if ((int) DirectionPlayer.West == pc.handFreeze)
            {
                Vector3 moveDirection = pc.handRight.position - this.transform.position;
                rb.AddForce(moveDirection * 300f);
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
            //Hand hand = player.GetComponent<Hand>();
            pc.isFreezeHand = true;
            
            
            IsDragNow = true;

            /*
            Collider collider = this.gameObject.GetComponent<Collider>();
            */
            
            
            if ((int)DirectionPlayer.East == pc.handFreeze)
            {
                if (GetComponent<Collider>() != null)
                {
                    SetHandInCenterObject(GetComponent<Collider>(),pc.handLeft);
                }
            }

            if ((int) DirectionPlayer.West == pc.handFreeze)
            {
                if (GetComponent<Collider>() != null)
                {
                    SetHandInCenterObject(GetComponent<Collider>(),pc.handRight);
                }
            }
        
            //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; 
            
            
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
            pc.handLeft.localPosition = pc.handLeftPos;
            pc.handRight.localPosition = pc.handRightPos;
            
            player.GetComponent<PlayerController>().isFreezeHand = false;
            
            
            
            IsDragNow = false;
            
            Debug.Log("Check Now Not Drag");
            // rb.isKinematic = false;
            return true;
        }
        return false;
    }
}
