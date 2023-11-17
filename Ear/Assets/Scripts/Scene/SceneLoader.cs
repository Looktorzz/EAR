using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField]private Animator _anim;

    public void StartGame()
    {
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(LevelIndex);
    }

    IEnumerator LoadLevel(int LevelIndex,float time)
    {
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(LevelIndex);
    }

    IEnumerator LoadLevel(string NameScene)
    {
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(NameScene);
    }

    IEnumerator LoadLevel(string NameScene,float time)
    {
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(NameScene);
    }

    IEnumerator ReLoadScene_WaitForSec()
    {
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        GameManager.instance.ReloadScene();
    }

    public void ReLoadScene()
    {
        StartCoroutine(ReLoadScene_WaitForSec());
    }

    public void LoadTransition()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadingErrorScene()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadToEndDemo()
    {
        StartCoroutine(LoadLevel(4));
    }

    public void LoadToNextSceneByName(string Name)
    {
        StartCoroutine(LoadLevel(Name));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
