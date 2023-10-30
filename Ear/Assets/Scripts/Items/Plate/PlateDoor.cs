using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlateDoor : MonoBehaviour
{
    [SerializeField] private MeasurePlate measurePlate;
    [SerializeField] private float slideSpeed = 1f;
    [SerializeField] private bool _isLimit;
    [SerializeField] private bool _isLower;
    [SerializeField] private bool _isRealtime;
    public bool _isCloseFromEvent;
    private bool _isOpen;
    
    [Header("Height")] 
    [SerializeField] private Transform closedPoint;
    [SerializeField] private Transform openedPoint;

    void Start()
    {
        measurePlate.GetComponent<MeasurePlate>();
        _isCloseFromEvent = false;
        _isOpen = false;
    }

    private void CheckTheDoor(bool isOpen)
    {
        if (_isOpen == !isOpen)
        {
            SoundManager.instance.Play(SoundManager.SoundName.GateOpen);
        }
        _isOpen = isOpen;
    }

    private void OpenDoor()
    {
        Vector3 targetPosition = Vector3.Lerp(closedPoint.position, openedPoint.position, measurePlate.maximumWeightForOpen);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
    }
    
    private void CloseDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, closedPoint.position, 5 * Time.deltaTime);
    }

    void Update()
    {
        float weightPercent = measurePlate.getWeightCurrent / measurePlate.maximumWeightForOpen;

        if (_isCloseFromEvent)
        {
            CheckTheDoor(false);
            CloseDoor();
            return;
        }
        
        if (_isLimit)
        {
            if (measurePlate.getWeightCurrent >= 5)
            {
                CheckTheDoor(true);
                OpenDoor();
            }   
            else
            {
                CheckTheDoor(false);
                CloseDoor();
            }
            
        }
        
        if (_isLower)
        {
            if ((measurePlate.maximumWeightForOpen - measurePlate.getWeightCurrent) >= 0)
            {
                CheckTheDoor(true);
                OpenDoor();
            }   
            else
            {
                CheckTheDoor(false);
                CloseDoor();
            }
            
        }
        
        if (_isRealtime)
        {
            if (measurePlate.getWeightCurrent > measurePlate.maximumWeightForOpen)
            {
                CheckTheDoor(true);
                OpenDoor();
            }
            else if (measurePlate.getWeightCurrent > 0)
            {
                CheckTheDoor(true);
                Vector3 targetPosition = Vector3.Lerp(closedPoint.position, openedPoint.position, weightPercent);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            }
            else if (measurePlate.getWeightCurrent == 0)
            {
                CheckTheDoor(false);
                CloseDoor();
            }
            
        }

    }
    
}
