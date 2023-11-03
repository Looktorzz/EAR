using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4_AfterFoorOpen : MonoBehaviour
{
    [SerializeField] GameObject Cam;
    private bool IsComplete = false;

    public void LookAtDoor()
    {
        if (!IsComplete)
        {
            StartCoroutine(waitForSec());
        }
        
    }


    IEnumerator waitForSec()
    {
        Cam.SetActive(true);
        yield return new WaitForSeconds(3f);
        IsComplete = true;
        Cam.SetActive(false);
    }
}
