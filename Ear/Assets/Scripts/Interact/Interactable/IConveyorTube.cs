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
    private bool isPlaceTube = false;
    private bool isPourWater = false;

    [Header("Animation")] 
    [SerializeField] private GameObject _waterPool;
    [SerializeField] private Animator _anim;
    [SerializeField] private TriggerEventAnimator _animEvent;

    public void DoAfterAnimationRun()
    {
        GameObject go = _itemPoint.GetChild(0).gameObject;
        if (go == null)
        {
            Debug.Log("Don't have any item in tube");
        }
                
        if (!isPlaceTube)
        {
            go.transform.position = _itemQuitFirst.position;
            Debug.Log("Item go FirstPosition");
        }
        else
        {
            go.transform.position = _itemQuitSecond.position;
            Debug.Log("Item go SecondPosition");
        }
                
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.Euler(Vector3.zero);
        go.transform.SetParent(null);
        go.GetComponent<Collider>().enabled = true;
        go.GetComponent<Rigidbody>().useGravity = true;
        go = null;
        
        _waterPool.SetActive(true);
    }
    

    public bool Interact(Interactor interactor)
    {
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
            if (bucket.isFull && !isPourWater)
            {
                SoundManager.instance.Play(SoundManager.SoundName.WaterFillPipe);
                _anim.SetTrigger("isPourWater");
                bucket.BucketIsFull(false);
                isPourWater = true;
                
                return true;
            }
        }

        // ++Sound fail (pak pak)
        SoundManager.instance.Play(SoundManager.SoundName.LeverImpact);

        return false;
    }
}
