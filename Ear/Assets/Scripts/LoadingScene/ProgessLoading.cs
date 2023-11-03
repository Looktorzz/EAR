using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgessLoading : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private GameObject firstError;
    
    // Start is called before the first frame update
    void Start()
    {
        _image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _image.fillAmount += 0.25f * Time.deltaTime;

        if (_image.fillAmount >= 0.85f)
        {
            _image.fillAmount = 0.85f;
            
            firstError.SetActive(true);
        }
    }
    
    
}
