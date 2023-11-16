using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IHoldGrabItem
{
    private BoxCollider _boxColliderAtPlayer;
    private BoxCollider _boxColliderAtBox;
    private Rigidbody _rb;
    private bool _isDragNow = false;

    [SerializeField] private bool isBasin;
    [SerializeField] private bool isBox;

    private void Start()
    {
        _boxColliderAtBox = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    public bool HoldInteract(Item item)
    {
        GameObject player = item.gameObject;
        
        if (!_isDragNow)
        {
            // ++Animation drag
            player.GetComponent<Item>().PlaceItem();
            player.GetComponent<PlayerController>().isFreezeHand = true;

            _isDragNow = true;
            transform.parent = player.transform;
            Debug.Log("Check Drag Now");
            
            CreatePlayerBoxCollider(player);
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _boxColliderAtBox.isTrigger = true;
            
            return true;
        }

        if (isBasin)
        {
            SoundManager.instance.Play(SoundManager.SoundName.DragBucket);
        }

        Debug.LogWarning("They are No Hold Interact");
        return false;
    }

    void CreatePlayerBoxCollider(GameObject player)
    {
        _boxColliderAtPlayer = player.AddComponent<BoxCollider>();
        
        Vector3 boxSize = _boxColliderAtBox.size;
        Vector3 boxCenter = _boxColliderAtBox.center;
        Vector3 boxLocalScale = transform.localScale;
        Vector3 boxLocalPos = transform.localPosition;
            
        _boxColliderAtPlayer.size = new Vector3(
            boxSize.x * boxLocalScale.x,
            boxSize.y * boxLocalScale.y,
            boxSize.z * boxLocalScale.z);

        _boxColliderAtPlayer.center = new Vector3(
            boxCenter.x + boxLocalPos.x, 
            ( boxCenter.y + boxLocalPos.y + player.transform.localPosition.y ) * 1.5f,
            boxCenter.z + boxLocalPos.z);
    }

    public void HoldCompleteInteract()
    {
        throw new System.NotImplementedException();
    }

    public bool ReleasedInteract(Item item)
    {
        GameObject player = item.gameObject;
        
        if(_isDragNow)
        {
            // --Animation drag
            player.GetComponent<PlayerController>().CheckDurationAnimation("IsHoldDrag", .25f, false);
            player.GetComponent<PlayerController>().isFreezeHand = false;

            Destroy(_boxColliderAtPlayer);
            _boxColliderAtBox.isTrigger = false;
            _rb.constraints = ~RigidbodyConstraints.FreezePositionY;
            
            _isDragNow = false;
            transform.parent = null;
            Debug.Log("Check Can't Drag Now");

            return true;
        }
        return false;
    }
}