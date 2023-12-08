using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ResolutionSetting : MonoBehaviour
{
    private Resolution[] resolutions;
    private List<Resolution> filterResolution;

    private float currentRefreshRate;
    private int currentResolutionIndex;
    
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
    int[] targetWidths = { 1280, 1440, 1600, 1920 };
    int[] targetHeights = { 720, 900, 900, 1080 };

    private int index;

    // Start is called before the first frame update
    void Start()
    {       
        DontDestroyOnLoad(this.gameObject);
            
        resolutionDropdown.ClearOptions();
        FillResolutionOptions();
    
        SetDropdown();
        //SetResolution(1920, 1080);

        
    }


    public void SetDefault()
    {
        SetDropdown();
        //SetResolution(1920, 1080);
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

            //SetResolution(targetWidth, targetHeight);
        }
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(targetWidths[index], targetHeights[index], Screen.fullScreen);
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

}
