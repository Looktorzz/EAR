using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public bool isPlayerInArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInArea = false;
        }
    }
}
