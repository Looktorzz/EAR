using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IHoldGrabItem
{
    bool IsDragNow = false;
    Rigidbody rb;
    PlayerController pc;

    [SerializeField] float PowerForce = 300f;
    [SerializeField] private bool isBasin;
    [SerializeField] private bool isBox;

    private Vector3 _sizeColliderOriginal;
    private Vector3 _sizeColliderChanged;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        _sizeColliderOriginal = GetComponent<BoxCollider>().size;
        _sizeColliderChanged = new Vector3(_sizeColliderOriginal.x * 0.9f,_sizeColliderOriginal.y,_sizeColliderOriginal.z * 0.9f);
    }

    private void FixedUpdate()
    {
        if (IsDragNow)
        {
            if ((int)DirectionPlayer.East == pc.handFreeze)
            {
                Vector3 moveDirection = pc.handRight.position - this.transform.position;
                rb.AddForce(moveDirection * PowerForce,ForceMode.Impulse);
                
                rb.constraints = RigidbodyConstraints.FreezeRotation;

            }

            if ((int) DirectionPlayer.West == pc.handFreeze)
            {
                Vector3 moveDirection = pc.handLeft.position - this.transform.position;
                rb.AddForce(moveDirection * PowerForce,ForceMode.Impulse);
                
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
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
            GameManager.instance.player.GetComponent<PlayerController>()._playerState = PlayerState.DragObject;
            
            GetComponent<BoxCollider>().size = _sizeColliderChanged;
            
            if ((int)DirectionPlayer.East == pc.handFreeze)
            {
                if (GetComponent<Collider>() != null)
                {
                    Debug.Log("Hold it");
                    Debug.LogError("TEST" + GetComponent<Collider>().name);
                    SetHandInCenterObject(GetComponent<Collider>(),pc.handRight);
                }
            }

            if ((int) DirectionPlayer.West == pc.handFreeze)
            {
                if (GetComponent<Collider>() != null)
                {
                    Debug.Log("Hold it");
                    Debug.LogError("TEST" + GetComponent<Collider>().name);
                    SetHandInCenterObject(GetComponent<Collider>(),pc.handLeft);
                }
            }
        
            rb.constraints = ~RigidbodyConstraints.FreezePositionY;
            
            
            Debug.Log("Check Now Drag");
            // rb.isKinematic = true;
            return true;
        }

        Debug.LogWarning("They are No Hold Interact");
        return false;
    }

    public void SetHandInCenterObject(Collider collider, Transform handPoint)
    {
        Debug.Log("Set Position");


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
            GetComponent<BoxCollider>().size = _sizeColliderOriginal;
            
            // --Animation drag
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.isFreezeHand = false;
            pc.handLeft.localPosition = pc.handLeftPos;
            pc.handRight.localPosition = pc.handRightPos;
            
            player.GetComponent<PlayerController>().isFreezeHand = false;
            
            rb.constraints = ~RigidbodyConstraints.FreezePositionY;
            
            IsDragNow = false;
            GameManager.instance.player.GetComponent<PlayerController>()._playerState = PlayerState.Idle;
            
            Debug.Log("Check Now Not Drag");
            // rb.isKinematic = false;
            return true;
        }
        return false;
    }
}
