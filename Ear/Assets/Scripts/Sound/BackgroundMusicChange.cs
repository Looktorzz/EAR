using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicChange : MonoBehaviour
{
    [SerializeField] bool StopMusic = false;

    // Start is called before the first frame update
    void Start()
    {
        if (StopMusic)
        {
            SoundManager.instance.Stop(SoundManager.SoundName.BGMFloor1);
        }
        else
        {
           SoundManager.instance.Play(SoundManager.SoundName.BGMFloor1);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
