using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFall : MonoBehaviour
{
    private Vector3 posItemFalling;
    [SerializeField] private float timeToFalling = 3;
    private Rigidbody rb;

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
        
        yield return new WaitForSeconds(timeToFalling);

        rb.constraints = ~RigidbodyConstraints.FreezeAll;

    }
    
}
