using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTime : MonoBehaviour
{
    [SerializeField] private float _startTime = 1;
    private float _currentTime;
    private bool _isCount = false;

    [SerializeField] private LayerMask _interactableMask;
    private Collider _collider = new Collider();
    private Hand _hand;
    
    private float _currentTimeDebug;

    public void Start()
    {
        _currentTime = _startTime;
        _hand = GetComponent<Hand>();

        _currentTimeDebug = _startTime;
    }

    public void FixedUpdate()
    {
        if (_isCount)
        {
            _currentTime -= 1 * Time.deltaTime;
            
            // Log
            if (_currentTimeDebug != (int)_currentTime)
            {
                _currentTimeDebug = (int)_currentTime;
                Debug.Log($"Time : {_currentTimeDebug:0}");
            }

            if (_currentTime <= 0)
            {
                _currentTime = 0;
                _isCount = false;
                CountComplete();
            }
        }
    }
    
    private void CountComplete()
    {
        _collider = _hand.SentColliderFound(_interactableMask);

        if(_collider != null)
        {
            IHoldInteractable holdInteractable = _collider.GetComponent<IHoldInteractable>();

            if(holdInteractable != null)
            {
                holdInteractable.HoldCompleteInteract();
            }

            _hand.ClearCollider();
        }
    }

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
