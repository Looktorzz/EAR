using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicChange : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.Play(SoundManager.SoundName.BGMFloor1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
