using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastInteract : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Hand hand;
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private Material _materialOutline;
    [SerializeField] private Material _materialDefault;
    [SerializeField] private LayerMask _layerMask;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (item.itemInHand != null)
        {
            switch (playerController.handFreeze)
            {
                case (int)DirectionPlayer.East:
                    RaycastCheckInteraction(Vector3.right);
                    break;
                case (int)DirectionPlayer.West:
                    RaycastCheckInteraction(Vector3.left);
                    break;
                case (int)DirectionPlayer.North:
                    RaycastCheckInteraction(Vector3.forward);
                    break;
                case (int)DirectionPlayer.South:
                    RaycastCheckInteraction(Vector3.back);
                    break;
            }
            
            
        }
    }

    private GameObject go;
    
    private void RaycastCheckInteraction(Vector3 direction)
    {
        Debug.Log(direction + "             Run");
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit,5f,_layerMask))
        {
            go = hit.collider.gameObject;
            ChangeMaterial(hit.collider.gameObject,_materialOutline);
            Debug.Log("Hit");
        }
        else 
        {
            if (go != null)
            {
                ChangeMaterial(go,_materialDefault);
                
                go = null;

            }
            
        }
    }

    private void ChangeMaterial(GameObject obj,Material material)
    {
        if (obj.GetComponentInChildren<SpriteRenderer>())
        {
            obj.GetComponentInChildren<SpriteRenderer>().material = material;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a red line in the positive x direction
        DrawRayGizmo(Vector3.right, Color.red);

        // Draw a green line in the negative x direction
        DrawRayGizmo(Vector3.left, Color.green);

        // Draw a blue line in the positive z direction
        DrawRayGizmo(Vector3.forward, Color.blue);

        // Draw a yellow line in the negative z direction
        DrawRayGizmo(Vector3.back, Color.yellow);

    }
    
    void DrawRayGizmo(Vector3 direction, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, direction * 100f);
    }

}
