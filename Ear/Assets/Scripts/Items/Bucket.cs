using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private GameObject _bucketFull;
    public bool isFull;

    private void Start()
    {
        isFull = false;
        _bucketFull.SetActive(false);
    }

    private void Update()
    {
        if (isFull)
        {
            _bucketFull.SetActive(true);
        }
        else
        {
            _bucketFull.SetActive(false);
        }
    }
}
