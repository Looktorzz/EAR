using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("PickUp Setting")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;


    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5f;
    [SerializeField] private float pickupForce = 450f;

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
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            //heldObjRB.useGravity = false;
            heldObjRB.drag = 0.3f;
            heldObjRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
        //heldObjRB.useGravity = true;
        heldObjRB.drag = 0f;
        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 direction = transform.TransformDirection(Vector3.left) * 5f;
        Gizmos.DrawRay(transform.position, direction);
    }

}
