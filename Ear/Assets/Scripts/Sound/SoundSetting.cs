using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    public static float BackGroundMusic { get; private set; }
    public static float SoundEffect { get; private set; }
    public static float AmbientSound { get; private set; }

    [SerializeField] private TextMeshProUGUI backGroundMusicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectSliderText;
    [SerializeField] private TextMeshProUGUI ambientSliderText;

    private void Update()
    {
        
    }

    public void BackgroundMusicValueChanged(float value)
    {
        BackGroundMusic = value;
        backGroundMusicSliderText.text = ((int)(value * 100)).ToString();
        SoundManager.instance.UpdateMixerVolumn();
    }

    public void SoundFXMusicValueChanged(float value)
    {
        SoundEffect = value;
        soundEffectSliderText.text = ((int)(value * 100)).ToString();
        SoundManager.instance.UpdateMixerVolumn();
    }
    
    public void AmbientSoundValueChanged(float value)
    {
        AmbientSound = value;
        ambientSliderText.text = ((int)(value * 100)).ToString();
        SoundManager.instance.UpdateMixerVolumn();
    }
}
