using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IConveyorTube : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private Transform _itemPoint;
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
            key.transform.SetParent(_itemPoint);
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
                GameObject go = _itemPoint.GetChild(0).gameObject;

                if (go == null)
                {
                    bucket.BucketIsFull(false);
                    Debug.Log("Don't have key on tube");
                    return false;
                }
                
                if (!isPlaceTube)
                {
                    // ++Sound item drop
                    go.transform.position = _itemQuitFirst.position;
                    go.transform.SetParent(null);
                    go.GetComponent<Collider>().enabled = true;
                    go.GetComponent<Rigidbody>().useGravity = true;
                    go = null;
                    Debug.Log("Item go FirstPosition");
                    
                    SoundManager.instance.Play(SoundManager.SoundName.WaterFillPipe);
                }
                else
                {
                    // ++Sound item drop
                    go.transform.position = _itemQuitSecond.position;
                    go.transform.SetParent(null);
                    go.GetComponent<Collider>().enabled = true;
                    go.GetComponent<Rigidbody>().useGravity = true;
                    go = null;
                    Debug.Log("Item go SecondPosition");
                    
                    SoundManager.instance.Play(SoundManager.SoundName.WaterFillPipe);
                }
                
                bucket.BucketIsFull(false);
                return true;
            }
            else
            {
                Debug.Log("Fill water in Bucket");
            }
        }

        // ++Sound fail (pak pak)
        SoundManager.instance.Play(SoundManager.SoundName.LeverImpact);

        return false;
    }
}
