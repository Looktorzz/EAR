using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _boardBreak;
    [SerializeField] private bool _isRight;
    private PlayerController _player;
    private Quaternion _rotationQuaternion;
    private Vector3 _rotationVector3;
    private bool _isUsedOnPlayer = false;

    private void Start()
    {
        _board.SetActive(true);
        _boardBreak.SetActive(false);
    }

    void Update()
    {
        if (gameObject.GetComponentInParent<PlayerController>() != null)
        {
            _isUsedOnPlayer = true;
            _player = gameObject.GetComponentInParent<PlayerController>();
            _rotationQuaternion = transform.rotation;

            switch (_player.handFreeze)
            {
                case (int)DirectionPlayer.East:
                    
                    _rotationVector3.y = -90;
                    Debug.Log("Left -90");
                    break;
                
                case (int)DirectionPlayer.West:
                    
                    _rotationVector3.y = 90;
                    Debug.Log("Right 90");
                    break;
                
                case (int)DirectionPlayer.North:
                case (int)DirectionPlayer.South:
                    
                    _rotationVector3.y = 0;
                    Debug.Log("Up Down 0");
                    break;
            }
            
            _rotationQuaternion.eulerAngles = _rotationVector3;
            this.transform.rotation = _rotationQuaternion;
            
            _player.gameObject.GetComponent<Item>().PlaceItem();
            // _player.gameObject.GetComponent<Item>().SetIdle();
            
        }
        
        if (gameObject.GetComponentInParent<PlayerController>() == null)
        {
            if (_isUsedOnPlayer)
            {
                _board.SetActive(false);
                _boardBreak.SetActive(true);
                
                _rotationVector3.x = 90;
                _rotationQuaternion.eulerAngles = _rotationVector3;
                this.transform.rotation = _rotationQuaternion;
                BoxCollider boxCollider = this.GetComponent<BoxCollider>();
                boxCollider.size = new Vector3(boxCollider.size.x,boxCollider.size.y,0.05f);
                
                this.GetComponent<BoxCollider>().excludeLayers = LayerMask.GetMask("Player");
                
                this.gameObject.layer = 0;
                this.gameObject.tag = "Untagged";
                this.enabled = false;
                
                SoundManager.instance.Play(SoundManager.SoundName.PlankDrop);

            }
        }
    }
}
