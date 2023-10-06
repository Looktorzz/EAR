using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlateDoor : MonoBehaviour
{
    [SerializeField] private MeasurePlate measurePlate;
    [SerializeField] private float maximumWeightForOpen = 5f;
    [SerializeField] private float slideSpeed = 1f;
    [SerializeField] private bool _isLimit;
    
    [Header("Height")] 
    [SerializeField] private Transform closedPoint;
    [SerializeField] private Transform openedPoint;
    
    void Start()
    {
        measurePlate.GetComponent<MeasurePlate>();
    }

    void Update()
    {
        float weightPercent = measurePlate.getWeightCurrent / maximumWeightForOpen;

        if (_isLimit)
        {
            if (measurePlate.getWeightCurrent == maximumWeightForOpen)
            {
                Vector3 targetPosition = Vector3.Lerp(closedPoint.position, openedPoint.position, maximumWeightForOpen);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, closedPoint.position, slideSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (measurePlate.getWeightCurrent > maximumWeightForOpen)
            {
                Vector3 targetPosition = Vector3.Lerp(closedPoint.position, openedPoint.position, maximumWeightForOpen);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            }
            else if (measurePlate.getWeightCurrent > 0)
            {
                Vector3 targetPosition = Vector3.Lerp(closedPoint.position, openedPoint.position, weightPercent);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            }
            else if (measurePlate.getWeightCurrent == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, closedPoint.position, slideSpeed * Time.deltaTime);
            }
        }

    }
    
}
