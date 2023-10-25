using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CloseDoor : MonoBehaviour
{
    [SerializeField] private DoorLever _doorLever;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _doorLever.CloseDoor();
        }
    }
}
