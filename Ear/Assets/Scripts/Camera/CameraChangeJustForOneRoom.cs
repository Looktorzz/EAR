using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeJustForOneRoom : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    private void Start()
    {
        Camera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Camera.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Camera.SetActive(false);
    }

}
