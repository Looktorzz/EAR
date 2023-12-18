using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private static Timer _instance;
    public static Timer instance => _instance;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] GameObject CanvasTimer;
    [SerializeField] bool timerEnabled = true;

    private static float _timer;
    public static float timer => _timer;
    
    private bool isTimerRunning = false;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CancelInvoke("UpdateTimer");
        }
        if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            InvokeRepeating("UpdateTimer",0,1);
        }

        if (SceneManager.GetActiveScene().name == "LevelBeforeTwo")
        {
            CancelInvoke("UpdateTimer");
        }

        if (SceneManager.GetActiveScene().name == "EndDemo")
        {
            CancelInvoke("UpdateTimer");
            UpdateTimerText();
        }
    }
    
    // Update is called once per frame
    void UpdateTimer()
    {
        _timer++;
        UpdateTimerText();
    }
    
    

    public void UpdateTimerText()
    {
        string minutes = Mathf.Floor(_timer / 60).ToString("00"); 
        string seconds = (_timer % 60).ToString("00");
        if (timerEnabled)
        {
            if (timerText != null)
            {
                timerText.text = $"{minutes}:{seconds}";
            }
        }
        
        
    }

    public void ResetTimer()
    {
        _timer = 0f;
    }

    public void TimerEnableDisable()
    {
        timerEnabled =! timerEnabled;

        if(CanvasTimer != null)
            CanvasTimer.gameObject.SetActive(timerEnabled);
        
    }

}
