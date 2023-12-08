using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class SoundSetting : MonoBehaviour
{
    public static float BackGroundMusic { get; private set; }
    public static float SoundEffect { get; private set; }

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmValue;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxValue;

    private bool isMuteBgm , isMuteSfx;
    
    private float tempBgmValue;
    private float tempSfxValue;
        
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
    
        


    #region PlaySound

    public void PlayButtonSound()
    {
        MainMenuSound.instance.Play(MenuSound.ButtonClick);
    }

    public void PlayVolumnSound()
    {
        MainMenuSound.instance.Play(MenuSound.VolumnClick);
    }


    #endregion
    
    #region BgmAndSfx
    
    public void BackgroundMusicValueChanged(float value)
    {
        if (!isMuteBgm)
        {
            BackGroundMusic = value;
            bgmValue.text = $"{Mathf.Round(BackGroundMusic * 100.0f)}";
            SoundManager.instance.UpdateMixerVolumn();
        }
        else
        {

        }
        
    }

    public void SoundFXMusicValueChanged(float value)
    {
        if (!isMuteSfx)
        {
            SoundEffect = value;
            
            sfxValue.text = $"{Mathf.Round(SoundEffect * 100.0f)}";
            SoundManager.instance.UpdateMixerVolumn();

        }
        else
        {

        }
    }
    
    public void MuteSfx(bool isMute)
    {
        if (isMute)
        {
            isMuteSfx = true;
            tempSfxValue = SoundEffect;
            SoundEffect = 0.001f;
            SoundManager.instance.UpdateMixerVolumn();
            sfxSlider.enabled = false;

        }
        else
        {
            SoundEffect = tempSfxValue;
            isMuteSfx = false;
            sfxSlider.enabled = true;
            SoundManager.instance.UpdateMixerVolumn();

        }
    }

    public void MuteBgm(bool isMute)
    {
        if (isMute)
        {
            isMuteBgm = true;
            tempBgmValue = BackGroundMusic;
            BackGroundMusic = 0.001f;
            SoundManager.instance.UpdateMixerVolumn();  
            bgmSlider.enabled = false;
        }
        else
        {
            BackGroundMusic = tempBgmValue;
            isMuteBgm = false;
            bgmSlider.enabled = true;
            SoundManager.instance.UpdateMixerVolumn();

        }
    }


    #endregion

    
    
}
