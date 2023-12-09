using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject resolutionMenu;
    [SerializeField] private GameObject soundMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                MainMenuSound.instance.Play(MenuSound.ButtonClick);
                pauseMenu.SetActive(true);
            }
            else
            {
                MainMenuSound.instance.Play(MenuSound.ButtonClick);
                pauseMenu.SetActive(false);
                resolutionMenu.SetActive(false);
                soundMenu.SetActive(false);
            }
        }
        
    }
}
