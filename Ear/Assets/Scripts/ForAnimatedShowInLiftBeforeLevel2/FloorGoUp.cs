using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloorGoUp : MonoBehaviour
{
    public float HowFarToGoUp;
    public float HowLongtoWait = 1f;

    private void Start()
    {
        StartCoroutine(Hello());
    }

    IEnumerator Hello()
    {
        yield return new WaitForSeconds(HowLongtoWait);
        transform.DOLocalMoveY(HowFarToGoUp,15f).SetEase(Ease.InBack);
    }

}
