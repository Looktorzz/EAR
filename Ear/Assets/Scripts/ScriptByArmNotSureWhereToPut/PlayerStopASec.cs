using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopASec : MonoBehaviour
{

    public void StartAnimation()
    {
        GameManager.instance.player.GetComponent<PlayerController>().PlayerStandStill();
    }

    public void StopAnimation()
    {
        GameManager.instance.player.GetComponent<PlayerController>().PlayerCanWalkNow();
    }


    
}
