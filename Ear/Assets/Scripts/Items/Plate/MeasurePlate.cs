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
    private bool isPlayerOnPlate = false;
    
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
        if (playerController.isGrabItem && isPlayerOnPlate)
        {
            
        }
    }

    private void UpdateText()
    {
        weightText.text = $"{(int)weightCurrent}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            isPlayerOnPlate = true;
            bucket = other.GetComponentInChildren<Bucket>();
            WeightIncrease(other);
            
            if (bucket != null)
            {
                WeightIncrease(bucket.gameObject.GetComponent<Collider>());
            }
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
            isPlayerOnPlate = false;

            WeightDecrease(other);
            WeightDecrease(bucket.gameObject.GetComponent<Collider>());

            bucket = null;

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
