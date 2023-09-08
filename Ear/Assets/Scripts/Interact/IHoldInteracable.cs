using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldInteractable
{ 
    public bool HoldInteract(Interactor interactor);
    public bool ReleasedInteract(Interactor interactor);
    
}
