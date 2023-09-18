using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour , IInteractable , IHoldInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    private Animator _animator;
    private CountdownTime _countdown;

    public bool Interact(Interactor interactor)
    {
        /*var Door = interactor.GetComponentInChildren<Keys>();

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
        */

        return false;
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool HoldInteract(Interactor interactor)
    {
        var Door = interactor.GetComponentInChildren<Keys>();
        _countdown = interactor.GetComponent<CountdownTime>();

        if (Door == null)
        {
            Debug.Log("YOU HAVE NO KEY TO OPEN!");
            return false;
        }

        if (Door.HasKey)
        {
            _countdown.Countdown(true);
            return true;
        }

        return false;
    }

    public void HoldCompleteInteract()
    {
        Debug.Log("YOU CAN OPEN DOOR!");
        _animator.SetTrigger("DoorOpen");
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        _countdown = interactor.GetComponent<CountdownTime>();
        _countdown.Countdown(false);
        return false;
    }
}
