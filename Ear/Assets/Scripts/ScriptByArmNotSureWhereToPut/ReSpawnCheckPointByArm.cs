using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnCheckPointByArm : MonoBehaviour
{
    public Transform checkPoint;
    private Collider _collider;
    public Room numRoom;


    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.ChangeCheckPoint(checkPoint,(int)numRoom);
            _collider.enabled = false;
        }
    }
}
