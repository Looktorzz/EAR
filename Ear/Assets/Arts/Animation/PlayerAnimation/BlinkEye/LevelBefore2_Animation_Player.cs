using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBefore2_Animation_Player : MonoBehaviour
{
    [SerializeField] Animator _animation;


    public void CloseEye()
    {
        _animation.SetTrigger("Close_Eye");
    }

    public void BlinkEye()
    {
        _animation.SetTrigger("Blink_Eye");
    }

    public void Dead()
    {
        _animation.SetBool("Dead",true);
    }

}
