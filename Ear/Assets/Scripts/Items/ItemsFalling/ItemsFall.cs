using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFall : MonoBehaviour
{
    private Vector3 posItemFalling;
    [SerializeField] private float timeToFalling = 3;
    private Rigidbody rb;
    [SerializeField] private AudioSource waterDrop;

    private bool isPlaySound = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        posItemFalling = gameObject.transform.position;
    }

    public void MoveToFirstPos()
    {
        StartCoroutine(DurationToDrop());
    }

    IEnumerator DurationToDrop()
    {
        this.gameObject.transform.position = posItemFalling;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        isPlaySound = false;
        yield return new WaitForSeconds(timeToFalling);

        rb.constraints = ~RigidbodyConstraints.FreezeAll;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaySound)
        {
            if (other.CompareTag("Water"))
            {
                waterDrop.Play();
                isPlaySound = true;
            }
        }
    }
}
