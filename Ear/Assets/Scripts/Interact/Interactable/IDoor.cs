using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    private Animator _animator;

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
}
