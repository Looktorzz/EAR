using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDoor : MonoBehaviour
{
    [SerializeField] private MeasurePlate measurePlate;

    [SerializeField] private float maximumWeightForOpen = 5f;
    [SerializeField] private float currentWeight;

    [SerializeField] private float slideSpeed = 1f;
    
    [Header("Height")] 
    [SerializeField] private float doorHeight = 2f;
    private Vector3 closedPosition;
    private Vector3 openedPosition;
    
    void Start()
    {
        measurePlate.GetComponent<MeasurePlate>();
        
        
        closedPosition = this.transform.position;
        openedPosition = this.transform.position + Vector3.up * doorHeight;
    }

    void Update()
    {
        float weightPercent = measurePlate.getWeightCurrent / maximumWeightForOpen;

        if (measurePlate.getWeightCurrent > maximumWeightForOpen)
        {
            Debug.Log("Morethan MaxmimumWeight");
            Vector3 targetPosition = Vector3.Lerp(closedPosition, openedPosition, maximumWeightForOpen);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
        }
        else if (measurePlate.getWeightCurrent > 0)
        {
            Debug.Log("Itworks > 0 ");

            Vector3 targetPosition = Vector3.Lerp(closedPosition, openedPosition, weightPercent);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            
        }
        else if (measurePlate.getWeightCurrent == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, slideSpeed * Time.deltaTime);
        }

    }
    
}
