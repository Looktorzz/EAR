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
            // ++Sound fail (pak pak)
            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            if (bucket.isAcidWater)
            {
                // ++Sound Destroy Box
                SoundManager.instance.Play(SoundManager.SoundName.Acid);
                
                if (_spawnGameObject != null)
                {
                    Instantiate(_spawnGameObject, _spawnPoint.position, Quaternion.identity);
                }
                bucket.BucketIsFull(false);
                
                GetComponentInChildren<Animator>().SetTrigger("isAcidBox");
                GetComponent<BoxCollider>().isTrigger = true;
                // this.gameObject.SetActive(false);
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

        // ++Sound fail (pak pak)
        return false;
    }
}
