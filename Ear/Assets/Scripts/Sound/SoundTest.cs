using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public void BGM()
    {
        SoundManager.instance.Play(SoundManager.SoundName.BackgroundMusic);
    }
    
    public void SFX()
    {
        SoundManager.instance.Play(SoundManager.SoundName.SoundEffect);
    }
}
