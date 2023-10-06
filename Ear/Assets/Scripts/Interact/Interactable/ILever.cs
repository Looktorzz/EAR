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
    
    [SerializeField] DoorCanPause _DoorcanPause;
    [SerializeField] bool _IsDoorSlowClose_AndCanStop = false;


    [Header("For Sound Only")]
    [SerializeField] private bool isBridge;
    [SerializeField] private bool isDoor;
    
    private void Start()
    {
        if(!_IsDoorSlowClose_AndCanStop) _animator = _doorLever.GetComponent<Animator>();
        
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
        SoundManager.instance.Play(SoundManager.SoundName.Lever);
        
        _isOpen = true;
        _close.SetActive(!_isOpen);
        _open.SetActive(_isOpen);
        
        if (_DoorcanPause != null)
        {
            _DoorcanPause.OpenDoor();
        }

        if(!_IsDoorSlowClose_AndCanStop) _animator.SetTrigger("OpenByLever");


        if (isBridge)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateRoll);
        }

        if (isDoor)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateNearlyClose);
        }
        
        return true;
        
        
    }
    
    public void DoorClose()
    {
        _isOpen = false;
        _close.SetActive(!_isOpen);
        _open.SetActive(_isOpen);
    }

}
