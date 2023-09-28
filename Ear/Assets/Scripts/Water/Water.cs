using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]public Transform waterSurface;

    
    [SerializeField] public bool directionBack;
    [SerializeField] public bool directionForward;
    [SerializeField] public bool directionLeft;
    [SerializeField] public bool directionRight;

    [SerializeField] private float waterForce = 100f;
    private Vector3 direction;
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BuoyancyObject>())
        {
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            direction = GetDirection();
            
            rb.AddForceAtPosition(direction * waterForce,other.gameObject.transform.position);
            
            //other.transform.position -= waterSurface.transform.localPosition;
            
            Debug.Log(other.name + "RigidbodyWork");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BuoyancyObject>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            direction = GetDirection();
            
            rb.velocity = Vector3.zero;
        }
    }

    public Vector3 GetDirection()
    {
        if (directionBack)
        {
            return Vector3.back;
        }
        if (directionForward)
        {
            return Vector3.forward;
        }

        if (directionLeft)
        {
            return Vector3.left;
        }

        if (directionRight)
        {
            return Vector3.right;
        }

        return Vector3.zero;
    }
    
}
