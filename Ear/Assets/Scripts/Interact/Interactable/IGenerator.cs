using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject _openGenerator;
    [SerializeField] private GameObject _closeGenerator;
    [SerializeField] private IFuseBox _fuseBox;
    [SerializeField] public List<GameObject> _lightGameObjects;

    private void Start()
    {
        _openGenerator.SetActive(false);
        _closeGenerator.SetActive(true);
    }

    public bool Interact(Interactor interactor)
    {
        if (_fuseBox.isHaveFuse)
        {
            // Open Light
            // ++Sound Open Light 
            for (int i = 0; i < _lightGameObjects.Count; i++)
            {
                _lightGameObjects[i].GetComponent<Light>().enabled = true;
            }
            
            _openGenerator.SetActive(true);
            _closeGenerator.SetActive(false);
            
            Debug.Log("Open light.");
            this.enabled = false;
            return true;
        }
        
        // ++Sound fail (pak pak)
        Debug.Log("Fill Fuse in Fuse Box First."); 
        return false;
    }

    
}
