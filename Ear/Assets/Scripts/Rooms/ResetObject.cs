using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private Vector3 originalPos;
    
    // Start is called be fore the first frame update
    void Awake()
    {
        originalPos = gameObject.transform.position;
    }

    private void OnDisable()
    {
        Debug.Log("Reset");

        transform.position = originalPos;
        
    }

    void Update()
    {
        
    }
}
