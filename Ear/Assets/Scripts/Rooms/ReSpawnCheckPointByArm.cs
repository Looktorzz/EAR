using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnCheckPointByArm : MonoBehaviour
{
    public Transform checkPoint;
    private Collider _collider;


    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.ChangeCheckPoint(checkPoint);
            _collider.enabled = false;
        }
    }
}
