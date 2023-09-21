using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private GameObject _bucketFullWater;
    [SerializeField] private GameObject _bucketFullAcidWater;
    public bool isFull;
    public bool isAcidWater;
    
    private void Start()
    {
        isFull = false;
        _bucketFullWater.SetActive(false);
        _bucketFullAcidWater.SetActive(false);
    }

    private void Update()
    {
        if (isFull)
        {
            if (isAcidWater)
            {
                _bucketFullAcidWater.SetActive(true);
            }
            else
            {
                _bucketFullWater.SetActive(true);
            }
        }
        else
        {
            _bucketFullWater.SetActive(false);
            _bucketFullAcidWater.SetActive(false);
        }
    }
}
