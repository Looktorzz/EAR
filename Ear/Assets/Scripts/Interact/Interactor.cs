using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    public /*readonly*/ Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    [SerializeField] private PlayerController _playerController;
    private float _currentTime = 3;
    private float _startTime = 3;
    private bool _isCount = false;
    private bool _isCountComplete = false;

    void Update()
    {
        // Delete when game complete
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
        _interactionPointRadius, _colliders, _interactableMask);
    }

    public void FixedUpdate()
    {
        if (_isCount)
        {
            _currentTime -= 1 * Time.deltaTime;
            Debug.Log($"Time : {_currentTime:0}");
            _isCountComplete = false;
            
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                _isCount = false;
                _isCountComplete = true;
            }
        }
    }

    public void CountdownTime(bool isTrue)
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

    public void HoldInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
            _interactionPointRadius, _colliders, _interactableMask);

        if(_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if(interactable != null && _isCountComplete)
            {
                interactable.Interact(this);
            } 
            /*if(interactable != null)
            {
                interactable.Interact(this);
            } */
        }
    }
    
    public void PressInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, 
            _interactionPointRadius, _colliders, _interactableMask);

        if(_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact(this);
            } 
        }
    }

    public void HoldInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position,
            _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IHoldInteractable>();

            if (interactable != null)
            {
                interactable.HoldInteract(this);
            }
        }
    }

    public void ReleasedHoldInteract()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position,
            _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IHoldInteractable>();

            if (interactable != null)
            {
                interactable.ReleasedInteract(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

}
