using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _doorTrigger;
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
            Debug.Log("Open light.");
            
            _openGenerator.SetActive(true);
            _closeGenerator.SetActive(false);
            this.enabled = false;
            
            Vector3 posDoor = _doorTrigger.transform.position;
            _doorTrigger.transform.DOLocalMoveY(posDoor.y + 1, 3).SetEase(Ease.OutBounce);
            
            return true;
        }
        
        // ++Sound fail (pak pak)
        Debug.Log("Fill Fuse in Fuse Box First."); 
        return false;
    }

    
}
