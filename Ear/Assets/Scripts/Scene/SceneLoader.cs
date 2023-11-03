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

    public void ExitGame()
    {
        Application.Quit();
    }
}
