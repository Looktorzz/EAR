using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] Transform positionReSpawns;
    float limitedDeadDepth = -15f;

    private void FixedUpdate()
    {
        if (gameObject.transform.position.y < limitedDeadDepth)
        {
            transform.position = positionReSpawns.position;
        }
    }
}
