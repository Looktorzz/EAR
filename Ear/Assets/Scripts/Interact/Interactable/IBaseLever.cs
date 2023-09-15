using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBaseLever : MonoBehaviour , IHoldInteractable , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private Transform _leverPosition;
    [SerializeField] private GameObject _doorLever;
    private CountdownTime _countdown;
    private Animator _animator;
    private Lever _lever;
    
    private void Start()
    {
        _animator = _doorLever.GetComponent<Animator>();
    }
    
    public bool Interact(Interactor interactor)
    {
        Lever lever = interactor.GetComponentInChildren<Lever>();
        Debug.Log("Test Click Lever");

        if (lever != null)
        {
            lever.transform.SetParent(_leverPosition);
            lever.transform.localPosition = Vector3.zero;
            lever.isOnBaseLever = true;
            return true;
        }

        return false;
    }
    
    public bool HoldInteract(Interactor interactor)
    {
        _lever = GetComponentInChildren<Lever>();
        _countdown = interactor.GetComponent<CountdownTime>();

        if (_lever != null)
        {
            _countdown.Countdown(true);
            return true;
        }

        Debug.Log("Don't have Lever");
        return false;
    }

    public void HoldCompleteInteract()
    {
        _lever.FlipYSpriteLever(true);
        Debug.Log("Lever Flip Y");
        
        _animator.SetTrigger("DoorOpen");
    }

    public bool ReleasedInteract(Interactor interactor)
    {
        _countdown = interactor.GetComponent<CountdownTime>();
        _countdown.Countdown(false);
        return false;
    }
}
