using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class FirstError : MonoBehaviour
{
    [SerializeField] private GameObject errorGroup;
    [SerializeField] private GameObject[] manyError;
    
    private float nextTimeError = 2f;
    private float currentTime = 0f;

    private void OnEnable()
    {
        this.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;

        if (currentTime >= nextTimeError)
        {
            currentTime = nextTimeError;
            
            for (int i = 0; i < manyError.Length; i++)
            {
                manyError[i].SetActive(true);
            }
            

        }
    }
    
}
