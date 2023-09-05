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
        var Door = interactor.GetComponent<Keys>();

        if (Door == null)
        {
            Debug.LogError("You have to add Keys Component to Player First");
            return false;
        }

        if (Door.HasKey)
        {
            Debug.Log("YOU CAN OPEN DOOR!");
            _animator.SetTrigger("DoorOpen");
            return true;
        }

        Debug.Log("YOU HAVE NO KEY TO OPEN!");
        return false;

    }

    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
