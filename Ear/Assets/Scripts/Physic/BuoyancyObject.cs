using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    
    [SerializeField]private float underWaterDrag = 3f;
    [SerializeField]private float underWaterAngularDrag = 1f;

    [SerializeField]private float airDrag = 0f;
    [SerializeField]private float airAngularDrag = 0.05f;

    [SerializeField]private float floatingPower = 15f;

    private Rigidbody m_Rigidbody;
    private bool underwater;


    private Water water;
    private bool isWater;
    
    // Start is called before the first frame update
    void Start()
    {
        isWater = false;
        m_Rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (water != null && isWater)
        {
            ItemFloatUp(water.waterSurface);
        }
    }

    void ItemFloatUp(Transform waterUpper)
    {
        float difference = this.gameObject.transform.position.y - waterUpper.position.y;

        if (difference < waterUpper.position.y)
        {
            float forceMultiplier = Mathf.Abs(difference) * floatingPower;
            
            m_Rigidbody.AddForceAtPosition(Vector3.up * forceMultiplier,this.gameObject.transform.position,ForceMode.Force);
                
            if (!underwater)
            { 
                underwater = true; 
                SwitchStates(true);
                
            }
        }

        if (underwater)
        {
            underwater = false;
            SwitchStates(false);
        }
    }

    void SwitchStates(bool isUnderwater)
    {
        if (isUnderwater)
        {
            m_Rigidbody.drag = underWaterDrag;
            m_Rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            m_Rigidbody.drag = airDrag;
            m_Rigidbody.angularDrag = airAngularDrag;
            m_Rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            water = other.GetComponent<Water>();
            isWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isWater = false;
        }
    }
}
