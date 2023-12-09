using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IDragableObject : MonoBehaviour, IHoldGrabItem
{
    private Rigidbody _rb;
    private bool _isDragNow = false;

    private FixedJoint _fixedJoint;
    private PlayerController _playerController;

    [SerializeField] private bool isBasin;
    [SerializeField] private bool isBox;



    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public bool HoldInteract(Item item)
    {
        GameObject player = item.gameObject;
        
        if (!_isDragNow)
        {
            // ++Animation drag
            player.GetComponent<Item>().PlaceItem();
            _playerController.isFreezeHand = true;

            _isDragNow = true;

            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _fixedJoint = player.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = _rb;
            
            Debug.Log("Check Drag Now");
            
            return true;
        }

        if (isBasin)
        {
            SoundManager.instance.Play(SoundManager.SoundName.DragBucket);
        }

        Debug.LogWarning("They are No Hold Interact");
        return false;
    }
    
    public void HoldCompleteInteract()
    {
        throw new System.NotImplementedException();
    }

    public bool ReleasedInteract(Item item)
    {
        if(_isDragNow)
        {
            // --Animation drag
            _playerController.CheckDurationAnimation("IsHoldDrag", .25f, false);
            _playerController.isFreezeHand = false;

            if (_fixedJoint != null)
            {
                Destroy(_fixedJoint);
            }
            
            _rb.constraints = ~RigidbodyConstraints.FreezePositionY;
            _isDragNow = false;
            
            Debug.Log("Check Can't Drag Now");

            return true;
        }
        return false;
    }
}