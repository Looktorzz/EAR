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
    private GameObject _key;
    private bool isPlaceTube;

    private void Start()
    {
        isPlaceTube = false;
    }

    // Yak Code Sa !!!!!!!! 
    public bool Interact(Interactor interactor)
    {
        /*// Key
        _key = interactor.GetComponentInChildren<Keys>().gameObject;
        if (_key != null)
        {
            _key.transform.SetParent(null);
            _key.transform.SetParent(_itemKeyPoint);
            _key.transform.localPosition = Vector3.zero;
        }

        // Tube (Can't Pick Again)
        Tube tube = interactor.GetComponentInChildren<Tube>();
        if (tube != null)
        {
            tube.transform.SetParent(null);
            tube.transform.SetParent(_itemTubePoint);
            tube.transform.localPosition = Vector3.zero;
            tube.gameObject.layer = 0; // Default ?? 
            isPlaceTube = true;
            
            Debug.Log($"tube.gameObject.layer : {tube.gameObject.layer}");
        }
        
        // Bucket
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();
        if (bucket != null)
        {
            if (bucket.isFull)
            {
                if (isPlaceTube)
                {
                    _key.transform.position = _itemQuitComplete.position;
                }
                else
                {
                    _key.transform.position = _itemQuitFail.position;
                }
                
                bucket.isFull = false;
                return true;
            }
            else
            {
                Debug.Log("Fill water in Bucket");
            }
        }*/

        return false;
    }
}
