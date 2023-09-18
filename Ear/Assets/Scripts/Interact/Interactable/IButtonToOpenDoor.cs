using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IButtonToOpenDoor : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    //[SerializeField] IDoorOpenFormAnother Door;
    [SerializeField] List<IDoorOpenFormAnother> Doors = new List<IDoorOpenFormAnother>();
    public bool Interact(Interactor interactor)
    {

        //Door.OnMoveUpByButton();
        foreach(var door in Doors)
        {
            door.OnMoveUpByButton();
        }
        return true;
    }

}
