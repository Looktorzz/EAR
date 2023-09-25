using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILever : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private GameObject _doorLever;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private bool _isOpen;
    
    private void Start()
    {
        _animator = _doorLever.GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    public bool Interact(Interactor interactor)
    {
        if (_isOpen)
        {
            return false;
        }
        
        _isOpen = true;
        _sprite.flipY = true;
        _animator.SetTrigger("OpenByLever");
        return true;
    }
}
