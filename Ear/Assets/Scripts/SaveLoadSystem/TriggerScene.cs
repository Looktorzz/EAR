using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] private string _nameScene;
    private SceneLoader _loader;

    private void Start()
    {
        _loader = GameObject.FindGameObjectWithTag("Scene_Loader").GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(_nameScene);
            _loader.LoadToNextSceneByName(_nameScene);
        }
    }
}
