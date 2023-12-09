using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IBinBox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public Transform _transform;
    public AudioSource sound;


    public bool Interact(Interactor interactor)
    {
        if (!interactor.GetComponent<Item>().isInteractFromSpace)
        {
            Bucket bucket = interactor.GetComponentInChildren<Bucket>();
            if (bucket != null)
            {
                interactor.GetComponentInChildren<Item>().PlaceItemOnInteractOnlyBin();
                bucket.gameObject.transform.position = _transform.position;
                sound.Play();
                return true;
            }
            Keys key = interactor.GetComponentInChildren<Keys>();
            if (key != null)
            {
                interactor.GetComponentInChildren<Item>().PlaceItemOnInteractOnlyBin();
                key.gameObject.transform.position = _transform.position;
                sound.Play();
                return true;
            }



            SoundManager.instance.Play(SoundManager.SoundName.Fail);
            return false;
        }
        
        // ++Sound fail (pak pak)
        SoundManager.instance.Play(SoundManager.SoundName.Fail);
        return false;
    }
}
