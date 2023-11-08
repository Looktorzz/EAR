using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
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
            }
        }
        
    }
}
