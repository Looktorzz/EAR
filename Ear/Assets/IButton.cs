using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _door;
    private bool _isStop = false;
    
    public bool Interact(Interactor interactor)
    {
        _isStop =  !_isStop;
        if (_isStop)
        {
            // Freeze
            Vector3 pos = _door.GetComponent<Transform>().position;
            _door.GetComponent<Transform>().position = pos;
        }
        else
        {
            // Down
            // _door.transform.DOMoveY()
        }

        return false;
    }
}
