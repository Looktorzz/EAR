using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private GameObject _bucketEmpty;
    [SerializeField] private GameObject _bucketFullWater;
    [SerializeField] private GameObject _bucketFullAcidWater;
    public bool isFull = false;
    public bool isAcidWater;
    
    private void Start()
    {
        _bucketEmpty.SetActive(true);
        _bucketFullWater.SetActive(false);
        _bucketFullAcidWater.SetActive(false);

    }

    private void Update()
    {
        if (isFull)
        {
            // ++Sound Add Water (Tak num)
            _bucketEmpty.SetActive(false);
            GetComponent<ObjectIndex>().ChangeIndex(NameObject.BucketFull);
            
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
            // ++Sound Delete Water (Tay num)
            _bucketEmpty.SetActive(true);
            _bucketFullWater.SetActive(false);
            _bucketFullAcidWater.SetActive(false);
            GetComponent<ObjectIndex>().ChangeIndex(NameObject.BucketEmpty);


        }
    }
}
