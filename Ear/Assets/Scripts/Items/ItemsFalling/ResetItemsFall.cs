using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItemsFall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ItemsFall>())
        {
            other.gameObject.GetComponent<ItemsFall>().MoveToFirstPos();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemsFall>())
        {
            other.GetComponent<ItemsFall>().MoveToFirstPos();
        }
    }
}
