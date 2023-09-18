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
    [SerializeField] private Transform _itemQuitFirst;
    [SerializeField] private Transform _itemQuitSecond;
    [SerializeField] private bool _isCanConnectTube = false;
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

        if (_isCanConnectTube)
        {
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
                
                if (!isPlaceTube)
                {
                    keyGameObject.transform.position = _itemQuitFirst.position;
                    keyGameObject.transform.SetParent(null);
                    keyGameObject = null;
                    Debug.Log("Key go FirstPosition");
                }
                else
                {
                    keyGameObject.transform.position = _itemQuitSecond.position;
                    keyGameObject.transform.SetParent(null);
                    keyGameObject = null;
                    Debug.Log("Key go SecondPosition");
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
