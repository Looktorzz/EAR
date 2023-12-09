using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickForMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI realTimer;
    [SerializeField] private TextMeshProUGUI showTimer;

    private void Start()
    {
        showTimer.text = realTimer.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
