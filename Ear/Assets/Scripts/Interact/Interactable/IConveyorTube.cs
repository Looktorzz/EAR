using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IConveyorTube : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private Transform _itemKeyPoint;
    [SerializeField] private Transform _itemTubePoint;
    [SerializeField] private Transform _itemQuitFail;
    [SerializeField] private Transform _itemQuitComplete;
    private bool isPlaceTube;

    private void Start()
    {
        isPlaceTube = false;
    }

    public bool Interact(Interactor interactor)
    {
        // Key
        Keys key = interactor.GetComponentInChildren<Keys>();
        if (key != null)
        {
            interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
            key.transform.SetParent(_itemKeyPoint);
            key.transform.localPosition = Vector3.zero;
        }

        // Tube (Can't Pick Again)
        Tube tube = interactor.GetComponentInChildren<Tube>();
        if (tube != null)
        {
            interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
            tube.transform.SetParent(_itemTubePoint);
            tube.transform.localPosition = Vector3.zero;
            tube.gameObject.layer = 0; 
            isPlaceTube = true;
        }
        
        // Bucket
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();
        if (bucket != null)
        {
            if (bucket.isFull)
            {
                GameObject keyGameObject = GetComponentInChildren<Keys>().gameObject;

                if (keyGameObject == null)
                {
                    bucket.isFull = false;
                    Debug.Log("Don't have key on tube");
                    return false;
                }
                
                if (isPlaceTube)
                {
                    keyGameObject.transform.position = _itemQuitComplete.position;
                    keyGameObject.transform.SetParent(null);
                    keyGameObject = null;
                    Debug.Log("Key go Complete");
                }
                else
                {
                    keyGameObject.transform.position = _itemQuitFail.position;
                    keyGameObject.transform.SetParent(null);
                    keyGameObject = null;
                    Debug.Log("Key go Fail");
                }
                
                bucket.isFull = false;
                return true;
            }
            else
            {
                Debug.Log("Fill water in Bucket");
            }
        }

        return false;
    }
}
