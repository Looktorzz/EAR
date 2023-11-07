using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuSound : MonoBehaviour
{
    [SerializeField] [CanBeNull] private SoundMainMenu[] _soundMain;

    [SerializeField] private AudioMixerGroup bgmMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private static MainMenuSound _instance;
    public static MainMenuSound instance => _instance;
    
    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        foreach (SoundMainMenu sound in _soundMain)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
            
            switch (sound.soundType)
            {
                case Sound.SoundType.BackgroundMusic:
                    sound.audioSource.outputAudioMixerGroup = bgmMixerGroup;
                    break;
                
                case Sound.SoundType.SoundFX:
                    sound.audioSource.outputAudioMixerGroup = sfxMixerGroup;
                    break;
            }
        }
        
        _instance = this;
        
        
        DontDestroyOnLoad(this.gameObject);
    }
    

    public void Play(MenuSound name)
    {

        SoundMainMenu sound = GetSound(name);
        
        if (sound.audioSource == null)
        {
            Debug.LogError("Sound :" + name);
            return;
        }
        
        sound.audioSource.Play();
    }
    
    

    private SoundMainMenu GetSound(MenuSound name)
    {
        return Array.Find(_soundMain, s => s.soundNameMenu == name);
    }

    
    
}

public enum MenuSound
{
    ButtonClick,
    Error,
    Downloading,
    VolumnClick,
}

[Serializable]public class SoundMainMenu
{
    [SerializeField] private MenuSound _soundName;
    public MenuSound soundNameMenu => _soundName;

    public Sound.SoundType soundType;

    [SerializeField] private AudioClip _clip;
    public AudioClip clip => _clip;

    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1f;
    public float volume => _volume;

    [SerializeField] private bool _loop;
    public bool loop => _loop;
    
    [HideInInspector]
    public AudioSource audioSource;
}
