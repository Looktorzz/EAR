using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ITube : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private GameObject _spawnGameObject;
    private bool isFilledWater;

    // Test
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _greenMaterial;

    private void Start()
    {
        isFilledWater = false;
    }

    public bool Interact(Interactor interactor)
    {
        if (isFilledWater)
        {
            // ++Sound fail (pak pak)
            return false;
        }
        
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket == null)
        {
            // ++Sound fail (pak pak)
            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            if (bucket.isAcidWater)
            {
                gameObject.GetComponent<MeshRenderer>().material = _greenMaterial;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = _blueMaterial;
            }
            
            Instantiate(_spawnGameObject, _spawnPoint.position, Quaternion.identity);
            bucket.BucketIsFull(false);
            isFilledWater = true;
            Debug.Log("Complete fill water");
            return true;
        }
        else
        {
            Debug.Log("Fill water in Bucket");
        }

        // ++Sound fail (pak pak)
        return false;
    }
}
