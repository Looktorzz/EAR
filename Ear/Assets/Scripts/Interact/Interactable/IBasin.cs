using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBasin : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _fullBasin;
    [SerializeField] private GameObject _almostFullBasin;
    [SerializeField] private GameObject _emptyBasin;
    private bool isFilledWater;
    private int _numFilled = 0;

    private void Start()
    {
        isFilledWater = false;
        _fullBasin.SetActive(false);
        _almostFullBasin.SetActive(false);
        _emptyBasin.SetActive(true);
    }

    public bool Interact(Interactor interactor)
    {
        if (isFilledWater)
        {
            return false;
        }
        
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket == null)
        {
            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            _numFilled++;

            switch (_numFilled)
            {
                case 2: // Almost
                    _fullBasin.SetActive(false);
                    _almostFullBasin.SetActive(true);
                    _emptyBasin.SetActive(false);
                    break;
                
                case 3: // Full
                    isFilledWater = true;
                    _fullBasin.SetActive(true);
                    _almostFullBasin.SetActive(false);
                    _emptyBasin.SetActive(false);
                    break;
            }
            
            bucket.isFull = false;
            Debug.Log($"Complete fill water = {_numFilled} times");
            return true;
        }
        else
        {
            Debug.Log("Fill water in Bucket");
        }

        return false;
    }
}
