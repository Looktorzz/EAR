using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManyError : MonoBehaviour
{

    [SerializeField]private float randomTime = 0;
    
    [SerializeField]private float currentTime = 0;

    [SerializeField] private SceneLoader _sceneLoader;
    private float timeToChangeScene = 7f;

    private bool isPlaySound = false;

    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;

        if (currentTime >= randomTime)
        {
            if (!isPlaySound)
            {
                isPlaySound = true;
                _audioSource.Play();
                this.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutSine);

            }
        }

        if (currentTime >= timeToChangeScene)
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.gameObject.SetActive(true);
                _sceneLoader.LoadToNextSceneByName("LevelBeforeOne");
            }

        }
    }
}
