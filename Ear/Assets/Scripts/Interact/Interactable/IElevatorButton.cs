using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class IElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _elevatorOff;
    [SerializeField] private GameObject _elevatorOn;
    [SerializeField] private GameObject _elevatorRed;
    [SerializeField] private GameObject _door;
    [SerializeField] private IFuseBox _iFuseBox;
    private bool _isCanUse;

    

    private void Start()
    {
        _elevatorOff.SetActive(false);
        _elevatorOn.SetActive(false);
        _elevatorRed.SetActive(true);
    }

    private void Update()
    {
        if (_iFuseBox.isHaveFuse & !_isCanUse)
        {
            _elevatorOff.SetActive(true);
            _elevatorOn.SetActive(false);
            _elevatorRed.SetActive(false);
            _isCanUse = true;
        }

    }

    public bool Interact(Interactor interactor)
    {
        if (_isCanUse)
        {
            _elevatorOff.SetActive(false);
            _elevatorOn.SetActive(true);
            _elevatorRed.SetActive(false);
            
            // ++Sound Open lift door
            Vector3 posDoor = _door.transform.position;
            _door.transform.DOLocalMoveX(posDoor.x + 1, 3).SetEase(Ease.InBack);
            this.enabled = false;
        }

        // ++Sound pak pak (Fail)
        return false;
    }
}
