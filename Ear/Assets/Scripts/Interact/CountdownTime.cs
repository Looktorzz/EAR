using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTime : MonoBehaviour
{
    [SerializeField] private float _startTime = 1;
    private float _currentTime;
    private bool _isCount = false;
    public bool isCountComplete = false;

    public void Start()
    {
        _currentTime = _startTime;
    }

    public void FixedUpdate()
    {
        if (_isCount)
        {
            _currentTime -= 1 * Time.deltaTime;
            Debug.Log($"Time : {_currentTime:0}");
            isCountComplete = false;
            
            // Have new solution ?? 
            if (_currentTime <= 0.05)
            {
                isCountComplete = true;
            }
            
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                _isCount = false;
                isCountComplete = false;
            }
        }
    }
    
    /*public void PressInteract()
    {
        _collider = _hand.SentColliderFound(_interactableMask);

        if(_collider != null)
        {
            IInteractable interactable = _collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact(this);
            }

            _hand.ClearCollider();
        }
    }*/

    public void Countdown(bool isTrue)
    {
        if (isTrue)
        {
            if (!_isCount)
            {
                _currentTime = _startTime;
                _isCount = true;
            }
        }
        else
        {
            _isCount = false;
        }
    }
}
