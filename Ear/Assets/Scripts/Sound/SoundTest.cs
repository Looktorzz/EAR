using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public void PlaySound(SoundManager.SoundName type)
    {
        SoundManager.instance.Play(type);
    }
    
    public void PlaySoundtest()
    {
        SoundManager.instance.Play(SoundManager.SoundName.LightOn);
    }
}
