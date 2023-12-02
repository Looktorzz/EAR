using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public static float BackGroundMusic { get; private set; }
    public static float SoundEffect { get; private set; }

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmValue;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxValue;

    private Resolution[] _resolutions;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
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
        
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].Equals(Screen.currentResolution))
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        Screen.SetResolution(1920,1080,Screen.fullScreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex]; 
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
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
        bgmValue.text = Mathf.Round(BackGroundMusic * 100.0f) + "%";
        SoundManager.instance.UpdateMixerVolumn();
        
    }

    public void SoundFXMusicValueChanged(float value)
    {
        SoundEffect = value;
        sfxValue.text = Mathf.Round(SoundEffect * 100.0f) + "%";

        SoundManager.instance.UpdateMixerVolumn();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
}
