using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDoor : MonoBehaviour
{
    [SerializeField] private MeasurePlate measurePlate;

    [SerializeField] private float maximumWeightForOpen;
    [SerializeField] private float currentWeight;

    [SerializeField] private float slideSpeed = 1f;
    
    [Header("Height")] 
    [SerializeField] private float doorHeight = 2f;
    private Vector3 closedPosition;
    private Vector3 openedPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        measurePlate.GetComponent<MeasurePlate>();
        
        
        maximumWeightForOpen = 5f;
        closedPosition = this.transform.position;
        openedPosition = this.transform.position + Vector3.up * doorHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float weightPercent = measurePlate.getWeightCurrent / maximumWeightForOpen;

        if (measurePlate.getWeightCurrent > maximumWeightForOpen)
        {
            Debug.Log("Morethan MaxmimumWeight");
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
