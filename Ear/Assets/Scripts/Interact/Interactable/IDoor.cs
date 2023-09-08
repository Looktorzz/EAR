using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour , IInteractable , IHoldInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    private Animator _animator;
    
    private float _currentTime = 2;
    private float _startTime = 2;
    private bool _isCount = false;
    private bool _isCountComplete = false;

    public bool Interact(Interactor interactor)
    {
        var Door = interactor.GetComponentInChildren<Keys>();

        if (Door == null)
        {
            Debug.Log("YOU HAVE NO KEY TO OPEN!");
            return false;
        }

        if (Door.HasKey)
        {
            Debug.Log("YOU CAN OPEN DOOR!");
            _animator.SetTrigger("DoorOpen");
            return true;
        }

        return false;

    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
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
                
                Debug.Log("YOU CAN OPEN DOOR!");
                _animator.SetTrigger("DoorOpen");
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

    public bool HoldInteract(Interactor interactor)
    {
        var Door = interactor.GetComponentInChildren<Keys>();

        if (Door == null)
        {
            Debug.Log("YOU HAVE NO KEY TO OPEN!");
            return false;
        }

        if (Door.HasKey)
        {
            CountdownTime(true);
            return true;
        }

        return false;
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        CountdownTime(false);
        return false;
    }
}
