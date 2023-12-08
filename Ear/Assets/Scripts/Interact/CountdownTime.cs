using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class CountdownTime : MonoBehaviour
{
    [SerializeField] private float _startTime = 1;
    private float _currentTime;
    private bool _isCount = false;

    [SerializeField] private LayerMask _interactableMask;
    private Collider _collider = new Collider();
    private Hand _hand;

    // Image
    [SerializeField] private GameObject _image;
    [SerializeField] private GameObject _imageBg;
    private Image _imageFill;
    private float _imageFillAmount;
    

    public void Start()
    {
        _currentTime = _startTime;
        _hand = GetComponent<Hand>();

        _image.gameObject.SetActive(false);
        _imageBg.gameObject.SetActive(false);
        _imageFill= _image.GetComponent<Image>();
    }

    public void FixedUpdate()
    {
        if (_isCount)
        {
            _currentTime -= 1 * Time.deltaTime;

            UIImageFill();

            if (_currentTime <= 0)
            {
                _currentTime = 0;
                _isCount = false;
                CountComplete();
            }
        }
        
        _image.gameObject.SetActive(_isCount);
        _imageBg.gameObject.SetActive(_isCount);
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

    private void UIImageFill()
    {
        _imageFillAmount = (_startTime - _currentTime) / _startTime;

        if (_imageFillAmount <= 0)
        {
            _imageFillAmount = 1;
        }
        
        _imageFill.fillAmount = _imageFillAmount;
        
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
