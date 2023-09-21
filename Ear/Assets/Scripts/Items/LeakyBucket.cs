using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LeakyBucket : Bucket
{
    [SerializeField] private TextMeshProUGUI massText;
    
    
    
    private Rigidbody rb;
    private float bucketMass;
    
    [SerializeField]private float maximumWaterWeight = 3f;
    [SerializeField]private float currentWaterWeight = 0f;
    
    private float speedWaterDecrease = 0.1f;
    
    
    
    private void Start()
    {
        isFull = false;

        rb = GetComponent<Rigidbody>();
        bucketMass = rb.mass;
    }
    
    private void Update()
    {
        massText.text = $"{(int) currentWaterWeight}";
        if (isFull)
        {
            currentWaterWeight = maximumWaterWeight;
            isFull = false;
        }
        else 
        {
            rb.mass = currentWaterWeight;
            
            if (currentWaterWeight > bucketMass)
            {
                currentWaterWeight -= speedWaterDecrease * Time.deltaTime;
                Debug.Log(currentWaterWeight);
            }
            else
            {
                currentWaterWeight = bucketMass;
            }

        }
    }
    
    
}
