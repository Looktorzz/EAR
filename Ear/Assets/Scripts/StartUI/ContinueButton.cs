using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{

    [SerializeField] private GameObject  GrayButton;
    [SerializeField] private GameObject  ColorButton;

    private static bool IsFirstTime = true;
    
    void Start()
    {
        if(!IsFirstTime){
            SetButtonColor();
        }
    }

        private void SetButtonGray()
    {
        GrayButton.SetActive(true);
        ColorButton.SetActive(false);

    }

    public void SetButtonColor()
    {
        IsFirstTime = false;
        ColorButton.SetActive(true);
        GrayButton.SetActive(false);

    }

}
