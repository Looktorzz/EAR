using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSound : MonoBehaviour
{
    public void PlaySound(MenuSound menuSound)
    {
        MainMenuSound.instance.Play(menuSound);
    }

    public void PlayButton()
    {
        MainMenuSound.instance.Play(MenuSound.ButtonClick);
    }

    public void PlayVolumnButton()
    {
        MainMenuSound.instance.Play(MenuSound.VolumnClick);
    }

    public void PlayErrorSound()
    {
        MainMenuSound.instance.Play(MenuSound.Error);
    }

    public void PlayDownloading()
    {
        MainMenuSound.instance.Play(MenuSound.Downloading);
    } 
}

