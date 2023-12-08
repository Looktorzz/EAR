using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorLever : MonoBehaviour
{
    [SerializeField] private bool _isOpen = false;
    private Vector3 positionStart;


    private void Start()
    {
        positionStart = transform.position;

        if (_isOpen)
        {
            
            transform.position = new Vector3(positionStart.x,positionStart.y + 4,positionStart.z);
            
            _isOpen = true;
        }
    }

    public void OpenDoor()
    {
        if (!_isOpen)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
            //transform.DOMoveY(transform.position.y + 4, 3).SetEase(Ease.OutBounce);
            transform.DOMove(new Vector3(positionStart.x, positionStart.y + 4, positionStart.z), 3f).SetEase(Ease.OutBounce);
            _isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
            //transform.DOMoveY(positionStart.position.y - 4, 2f).SetEase(Ease.OutBounce);
            transform.DOMove(positionStart, 2f).SetEase(Ease.OutBounce);
            
            _isOpen = false;
        }
    }
}
