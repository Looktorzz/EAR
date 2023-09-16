using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private SpriteRenderer _spriteLever;
    public bool isOnBaseLever;

    private void Start()
    {
        _spriteLever = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isOnBaseLever)
        {
            _spriteLever.flipY = false;
        }
        else
        {
            if (GetComponentInParent<Hand>() != null)
            {
                isOnBaseLever = false;
            }
        }
    }

    public void FlipYSpriteLever(bool isTrue)
    {
        _spriteLever.flipY = isTrue;
    }
}
