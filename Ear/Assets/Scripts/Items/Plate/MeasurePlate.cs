using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeasurePlate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weightText;

    [SerializeField] private float weightCurrent;
    public float getWeightCurrent => weightCurrent;

    private PlayerController playerController;
    
    private Item item;
    private Rigidbody rb;
    private Bucket bucket;

    private float enterMass;
    
    private void Start()
    {
        UpdateText();
        
        GameObject go = GameObject.FindWithTag("Player");
        if (go != null)
        {
            playerController = go.GetComponent<PlayerController>();
        }
        
    }

    private void Update()
    {
        UpdateText();

    }
    
    private void UpdateText()
    {
        weightText.text = $"{(int)weightCurrent}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            WeightIncrease(other);

        }

        if (other.CompareTag("Item"))
        {
            WeightIncrease(other);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeightDecrease(other);
        }
        
        if (other.CompareTag("Item"))
        {
            WeightDecrease(other);
        }

    }

    private void WeightIncrease(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        
        weightCurrent += rb.mass;
        UpdateText();
    }

    private void WeightDecrease(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        
        weightCurrent -= rb.mass;
        
        UpdateText();

    }
   
}
