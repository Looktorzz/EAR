using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    //[SerializeField] private Transform[] floaters;
    [SerializeField] private int floatersUnderwater;
    
    [SerializeField]private float underWaterDrag = 3f;
    [SerializeField]private float underWaterAngularDrag = 1f;

    [SerializeField]private float airDrag = 0f;
    [SerializeField]private float airAngularDrag = 0.05f;

    [SerializeField]private float floatingPower = 15f;

    private Rigidbody m_Rigidbody;
    private bool underwater;

    [SerializeField]private float waterHeight = 0f;

    [SerializeField] private Transform waterUpper;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderwater = 0;
        /*
        for (int i = 0; i < floaters.Length; i++)
        {
            */
            float difference = this.gameObject.transform.position.y - waterHeight;

            if (difference < waterUpper.position.y)
            {
                m_Rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference),this.gameObject.transform.position,ForceMode.Force);
                
                floatersUnderwater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchStates(true);
                }
            }

        //}
        
        if (underwater && floatersUnderwater == 0)
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
        }
    }
    
    
}
