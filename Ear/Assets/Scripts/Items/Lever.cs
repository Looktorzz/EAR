using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject _close;
    [SerializeField] private GameObject _open;
    public bool isOnBaseLever;

    private void Start()
    {
        SetLeverOpen(false);
    }

    private void Update()
    {
        if (!isOnBaseLever)
        {
            SetLeverOpen(false);
        }
        else
        {
            if (GetComponentInParent<Hand>() != null)
            {
                isOnBaseLever = false;
            }
        }
    }

    public void SetLeverOpen(bool isOpen)
    {
        _close.SetActive(!isOpen);
        _open.SetActive(isOpen);
    }
}
