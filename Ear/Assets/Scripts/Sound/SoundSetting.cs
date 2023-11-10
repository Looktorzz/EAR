using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public static float BackGroundMusic { get; private set; }
    public static float SoundEffect { get; private set; }

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        BackGroundMusic = 1f;
        SoundEffect = 1f;
    }

    private void Start()
    {
        bgmSlider.value = BackGroundMusic;
        sfxSlider.value = SoundEffect;
        
    }

    public void PlayButtonSound()
    {
        MainMenuSound.instance.Play(MenuSound.ButtonClick);
    }

    public void PlayVolumnSound()
    {
        MainMenuSound.instance.Play(MenuSound.VolumnClick);
    }

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
