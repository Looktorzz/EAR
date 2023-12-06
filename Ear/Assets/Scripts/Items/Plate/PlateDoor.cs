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
    [SerializeField] private bool _isOpen;
    
    [Header("Height")] 
    [SerializeField] private Transform closedPoint;
    [SerializeField] private Transform openedPoint;

    [Header("Door Option")]
    [SerializeField] bool IsDoor = false;
    [SerializeField] private DoorLever door;
    

    void Start()
    {
        measurePlate.GetComponent<MeasurePlate>();
        _isCloseFromEvent = false;
    }

    private void CheckTheDoor(bool isOpen)
    {
        if (_isOpen == !isOpen)
        {
            if(!IsDoor) SoundManager.instance.Play(SoundManager.SoundName.GateOpen);

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
            if (!IsDoor)
            {
                CloseDoor();
            }
            else door.CloseDoor();

            return;
        }
        
        if (_isLimit)
        {
            if (!IsDoor)
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
            else
            {
                if (measurePlate.getWeightCurrent == measurePlate.maximumWeightForOpen)
                {
                    CheckTheDoor(true);
                    door.OpenDoor();
                }
                else
                {
                    CheckTheDoor(false);
                    door.CloseDoor();
                }
            }


        }
        
        if (_isLower)
        {
            if ((measurePlate.maximumWeightForOpen - measurePlate.getWeightCurrent) >= 0)
            {
                CheckTheDoor(true);
                if (!IsDoor)
                {
                    OpenDoor();
                }
                else door.OpenDoor();
            }   
            else
            {
                CheckTheDoor(false);
                if (!IsDoor)
                {
                    CloseDoor();
                }
                else door.CloseDoor();
                
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
