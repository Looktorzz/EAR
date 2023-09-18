using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBrokenBox : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private GameObject _spawnGameObject;
    [SerializeField] private Transform _spawnPoint;
    public bool Interact(Interactor interactor)
    {
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket == null)
        {
            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            if (bucket.isAcidWater)
            {
                Instantiate(_spawnGameObject, _spawnPoint.position, Quaternion.identity);
                bucket.isFull = false;
                
                Destroy(gameObject);
                return true;
            }
            else
            {
                Debug.Log("Can't destroy by water");
            }
        }
        else
        {
            Debug.Log("Fill water in Bucket");
        }

        return false;
    }
}