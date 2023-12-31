using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLightTrigger : MonoBehaviour
{
    [SerializeField] private IGenerator _generator;
    [SerializeField] private DoorLever door;
    private bool _isPass = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_isPass)
        {
            // Close Light
            // ++Sound Close Light
            // ??Sound PaKaiFire Where??
            SoundManager.instance.Play(SoundManager.SoundName.BlackOut);
            
            door.CloseDoor();

            for (int i = 0; i < _generator._lightGameObjects.Count; i++)
            {
                _generator._lightGameObjects[i].GetComponentInChildren<Light>().enabled = false;
                _isPass = true;
            }
            
        }
    }
}
