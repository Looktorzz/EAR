using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    public static float BackGroundMusic { get; private set; }
    public static float SoundEffect { get; private set; }

    public void BackgroundMusicValueChanged(float value)
    {
        
        BackGroundMusic = value;
        SoundManager.instance.UpdateMixerVolumn();
        
    }

    public void SoundFXMusicValueChanged(float value)
    {
        SoundEffect = value;
        SoundManager.instance.UpdateMixerVolumn();
    }
    
}
