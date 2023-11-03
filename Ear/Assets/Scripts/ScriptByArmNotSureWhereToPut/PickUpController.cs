using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("PickUp Setting")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Hand")] 
    [SerializeField] private Transform rightTarget;
    [SerializeField] private Transform leftTarget;
    [SerializeField] private Vector3 rightTargetPos;
    [SerializeField] private Vector3 leftTargetPos;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5f;
    [SerializeField] private float pickupForce = 250f;


    private void Awake()
    {
        rightTargetPos = rightTarget.localPosition;
        leftTargetPos = leftTarget.localPosition;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(heldObj == null)
            {
                Debug.Log("It Try");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                    Debug.Log("HIT!!");
                    Debug.LogWarning(hit.collider.gameObject.name);
                }
            }
            else
            {
                DropObject();
            }
        }
        if(heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f &&
            Vector3.Distance(heldObj.transform.position, holdArea.position) <= 1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
        else if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 1f)
        {
            //DropObject();
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Collider objectCollider = pickObj.GetComponent<Collider>();

            if (objectCollider != null)
            {
                Vector3 centerBottom = objectCollider.bounds.center;
                centerBottom.y = objectCollider.bounds.min.y;

                rightTarget.position = centerBottom;
            }
            
            
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            //heldObjRB.useGravity = false;
            heldObjRB.drag = 0.1f;
            heldObjRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;

            
        }
    }

    void DropObject()
    {
        //heldObjRB.useGravity = true;
        heldObjRB.drag = 0f;
        heldObjRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
        heldObjRB.transform.parent = null;
        heldObj = null;

        leftTarget.position = leftTargetPos;
        //rightTarget.position = rightTargetPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 direction = transform.TransformDirection(Vector3.left) * pickupRange;
        Gizmos.DrawRay(transform.position, direction);
    }

}
