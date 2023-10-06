using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorCanPause : MonoBehaviour
{
    public bool IsDoorOpen => _IsDoorOpen;
    [SerializeField] private bool _IsDoorOpen;

    private bool IsPauseDoor = false;

    Vector3 decressDoor = new Vector3(0,0.1f,0);
    [SerializeField]float speedClose = 10f;
    float limitOpen = 5f;
    Vector3 pos;

    [SerializeField] ILever _lever;

    private void Start()
    {
        pos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!IsPauseDoor)
        {
            if (_IsDoorOpen)
            {
                transform.position -= decressDoor * Time.fixedDeltaTime * speedClose;

                if (transform.position.y < pos.y)
                {
                    _IsDoorOpen = false;
                    _lever.DoorClose();
                    transform.position = pos;
                }
            }
        }    
        
    }

    public void OpenDoor()
    {
        if (!IsPauseDoor)
        {
            if (!_IsDoorOpen)
            {
                JustFirstTimeOpenDoor();

                _IsDoorOpen = true;
            }
        }
    }

    public void PauseDoor()
    {
        IsPauseDoor = true;
    }

    private void JustFirstTimeOpenDoor()
    {
        transform.DOMoveY(pos.y+limitOpen, 2f).SetEase(Ease.OutQuint);
    }

}
