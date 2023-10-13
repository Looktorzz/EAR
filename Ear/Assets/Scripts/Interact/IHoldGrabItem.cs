using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldGrabItem
{
    public bool HoldInteract(Item item);
    public void HoldCompleteInteract();
    public bool ReleasedInteract(Item item);
}
