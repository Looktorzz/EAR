using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorLever : MonoBehaviour
{
    public void OpenDoor()
    {
        Vector3 posDoor = transform.position;
        transform.DOLocalMoveY(posDoor.y + 1, 3).SetEase(Ease.OutBounce);
    }
}
