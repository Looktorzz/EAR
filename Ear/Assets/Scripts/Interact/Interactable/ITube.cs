using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITube : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }
}
