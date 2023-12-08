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
    
    private Resolution[] resolutions;
    private List<Resolution> filterResolution;

    private float currentRefreshRate;
    private int currentResolutionIndex;
    
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
    int[] targetWidths = { 1280, 1440, 1600, 1920 };
    int[] targetHeights = { 720, 900, 900, 1080 };

    
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
        
        resolutionDropdown.ClearOptions();
        FillResolutionOptions();
        
        SetDropdown();
        SetResolution(1920, 1080);
        
    }
    
    
    public void SetDefault()
    {
        SetDropdown();
        SetResolution(1920, 1080);
    }

    

    #region Resolution

    void SetDropdown()
    {
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.value = resolutionDropdown.options.FindIndex(option => option.text == "1920x1080");
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }
    
    void FillResolutionOptions()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = 0; i < targetWidths.Length; i++)
        {
            string option = targetWidths[i] + "x" + targetHeights[i];
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    void OnResolutionChanged(int index)
    {
        resolutions = Screen.resolutions;
        if (index >= 0 && index < resolutions.Length)
        {
            int targetResolutionIndex = Mathf.Clamp(index, 0, targetWidths.Length - 1);
            int targetWidth = targetWidths[targetResolutionIndex];
            int targetHeight = targetHeights[targetResolutionIndex];

            SetResolution(targetWidth, targetHeight);
        }
    }

    void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    #endregion

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
