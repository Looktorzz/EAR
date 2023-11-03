using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CloseDoor : MonoBehaviour
{
    [SerializeField] private DoorLever _doorLever;
    [SerializeField] private PlateDoor _plateDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_doorLever != null)
            {
                _doorLever.CloseDoor();
            }
            
            if (_plateDoor != null)
            {
                _plateDoor._isCloseFromEvent = true;
                // _plateDoor.CheckTheDoor(false);
            }
        }
    }
}
