using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventAnimator : MonoBehaviour
{
    // [SerializeField] private IInteractable _interactor;
    [SerializeField] private IConveyorTube _conveyor;
    
    public void CheckAnim()
    {
        _conveyor.DoAfterAnimationRun();
    }
}
