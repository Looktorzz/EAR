using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorLever : MonoBehaviour
{
    [SerializeField] private bool _isOpen = false;

    private void Start()
    {
        if(_isOpen)
        {
            transform.DOMoveY(transform.position.y + 4, 3).SetEase(Ease.OutBounce);
            _isOpen = true;
        }
    }

    public void OpenDoor()
    {
        if (!_isOpen)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
            transform.DOMoveY(transform.position.y + 4, 3).SetEase(Ease.OutBounce);
            _isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
            transform.DOMoveY(transform.position.y - 4, 2f).SetEase(Ease.OutBounce);
            _isOpen = false;
        }
    }
}
