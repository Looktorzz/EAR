using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup bgmMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private AudioMixerGroup ambientMixerGroup;
    
    [SerializeField] private Sound[] _sounds;

    
    private static SoundManager _instance;
    public static SoundManager instance => _instance;
    
    
    public enum SoundName
    {
        WoodDrop,
        Lever,
        DragBucket,
        WaterDrip,
        WaterFill,
        GateOpen,
        GateClose,
        WaterDrop,
        WaterDrop2,
        WaterFillPipe,
        WaterFillPipe2,
        ConnectPipe,
        DragPipe,
        PressSwitch,
        LeverImpact,
        LeverImpact2,
        Generator,
        InsertFuse,
        LightOn,
        WaterSplash,
        Acid,
        GateRoll,
        GateNearlyClose,
        
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        foreach (Sound sound in _sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.pitch = sound.pitch;
            
            switch (sound.soundType)
            {
                case Sound.SoundType.BackgroundMusic:
                    sound.audioSource.outputAudioMixerGroup = bgmMixerGroup;
                    break;
                
                case Sound.SoundType.SoundFX:
                    sound.audioSource.outputAudioMixerGroup = sfxMixerGroup;
                    break;
                
                case Sound.SoundType.Ambient:
                    sound.audioSource.outputAudioMixerGroup = ambientMixerGroup;
                    break;
            }
        }
        
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    
    public void Play(SoundName name)
    {

        Sound sound = GetSound(name);
        
        if (sound.audioSource == null)
        {
            Debug.LogError("Sound :" + name);
            return;
        }

        sound.audioSource.Play();
    }
    
    

    private Sound GetSound(SoundName name)
    {
        return Array.Find(_sounds, s => s.soundName == name);
    }

    public void UpdateMixerVolumn()
    {
        bgmMixerGroup.audioMixer.SetFloat("BGM", Mathf.Log10(SoundSetting.BackGroundMusic) * 20);
        sfxMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(SoundSetting.SoundEffect) * 20);
        ambientMixerGroup.audioMixer.SetFloat("Ambient", Mathf.Log10(SoundSetting.AmbientSound) * 20);
    }

    public void Mute()
    {
        
    }
}

[Serializable]public class Sound
{
    [SerializeField] private SoundManager.SoundName _soundName;
    public SoundManager.SoundName soundName => _soundName;

    public enum SoundType
    {
        BackgroundMusic,
        SoundFX,
        Ambient,
    }

    public SoundType soundType;

    [SerializeField] private AudioClip _clip;
    public AudioClip clip => _clip;

    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1f;
    public float volume => _volume;

    [SerializeField] private bool _loop;
    public bool loop => _loop;
    
    [Range(0f,1f)]
    [SerializeField] private float _pitch = 1;
    public float pitch => _pitch;
    
    [HideInInspector]
    public AudioSource audioSource;
}
