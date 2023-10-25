using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorLever : MonoBehaviour
{
    private bool _isClose;

    private void Start()
    {
        _isClose = true;
    }

    public void OpenDoor()
    {
        if (_isClose)
        {
            transform.DOMoveY(transform.position.y + 4, 3).SetEase(Ease.OutBounce);
            _isClose = false;
        }
    }

    public void CloseDoor()
    {
        if (!_isClose)
        {
            transform.DOMoveY(transform.position.y - 4, 2f).SetEase(Ease.OutBounce);
            _isClose = true;
        }
    }
}
