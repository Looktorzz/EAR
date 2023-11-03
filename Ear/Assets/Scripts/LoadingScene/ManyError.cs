using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManyError : MonoBehaviour
{

    [SerializeField]private int randomTime = 0;
    
    [SerializeField]private float currentTime = 0;

    [SerializeField] private SceneLoader _sceneLoader;
    private float timeToChangeScene = 7f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        randomTime = Random.Range(1, 3);
    }

    private void Awake()
    {
    }

    private void OnEnable()
    {
        this.transform.DOScale(Vector3.one, randomTime).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;

        if (currentTime >= timeToChangeScene)
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.gameObject.SetActive(true);
                _sceneLoader.LoadTransition();
            }

        }
    }
}
