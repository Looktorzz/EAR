using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILever : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private GameObject _doorLever;
    [SerializeField] private GameObject _close;
    [SerializeField] private GameObject _open;
    private Animator _animator;
    private bool _isOpen;
    
    private void Start()
    {
        _animator = _doorLever.GetComponent<Animator>();
        
        _isOpen = false;
        _close.SetActive(!_isOpen);
        _open.SetActive(_isOpen);
    }
    
    public bool Interact(Interactor interactor)
    {
        if (_isOpen)
        {
            // ++Sound fail (pak pak)
            return false;
        }
        
        // Open ( If have more 1 sound or anim / Add variable at upper c: )
        // ++Sound open door by interact lever
        _isOpen = true;
        _close.SetActive(!_isOpen);
        _open.SetActive(_isOpen);
        _animator.SetTrigger("OpenByLever");
        return true;
    }
}
