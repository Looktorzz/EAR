using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFuseBox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private Transform _fusePosition;
    public bool _isHaveFuse = false;
    
    public bool Interact(Interactor interactor)
    {
        Fuse fuse = interactor.GetComponentInChildren<Fuse>();
        Debug.Log("Test Click Lever");

        if (fuse != null)
        {
            interactor.GetComponentInChildren<Item>().PlaceItemOnInteract();
            fuse.transform.SetParent(_fusePosition);
            fuse.transform.localPosition = Vector3.zero;
            _isHaveFuse = true;
            Debug.Log("Complete Fill Fuse.");
            
            return true;
        }

        return false;
    }
}
