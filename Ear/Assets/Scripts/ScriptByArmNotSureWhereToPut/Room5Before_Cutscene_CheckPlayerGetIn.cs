using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5Before_Cutscene_CheckPlayerGetIn : MonoBehaviour
{
    private bool IsFinishAnimation = false;
    public DoorLever closeit;
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!IsFinishAnimation)
            {
                animator.SetTrigger("Player_ComeIn");
                closeit.CloseDoor();
                IsFinishAnimation = true;
            }
            
        }
    }

}
