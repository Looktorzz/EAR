using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeasurePlate : MonoBehaviour
{
    [SerializeField] private ObjectDataSO _objectDataSo;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private float weightCurrent;
    public float getWeightCurrent => weightCurrent;
    
    private GameObject _player;
    private GameObject _itemInHand;
    private PlayerController _playerController;
    private bool _isTrigger = false;
    private bool _isAdd = false;

    [SerializeField] private GameObject _boxPhysic;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Collider[] _colliders = new Collider[10];
    
    private void Start()
    {
        // UpdateText();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _colliders = null;
    }
    
    private void FoundCollider()
    {
        Physics.OverlapBoxNonAlloc(_boxPhysic.transform.position, _boxPhysic.transform.localScale, 
            _colliders, Quaternion.identity, _layerMask);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_boxPhysic.transform.position, _boxPhysic.transform.localScale);
    }

    private float _weight;

    private void PlusWeight()
    {
        _weight = 0;

        for (int i = 0; i < _colliders.Length; i++)
        {
            int index = _colliders[i].GetComponent<ObjectIndex>().index;
            _weight += _objectDataSo.objectDatas[index].weight;
        }
        
        Array.Clear(_colliders,0,_colliders.Length);
    }

    private void Update()
    {
        FoundCollider();
        if (_colliders != null)
        {
            PlusWeight();
            weightText.text = _weight.ToString("0");
        }

        /*UpdateText();

        if (_isTrigger)
        {
            if (_playerController.isGrabItem)
            {
                _itemInHand = _player.GetComponent<Item>().itemInHand;
                
                if (_itemInHand != null && !_isAdd)
                {
                    Debug.Log("Add weight");
                    WeightIncrease(_itemInHand.GetComponent<Collider>());
                    _isAdd = true;
                }
            }
            else
            {
                if (_itemInHand != null && _isAdd)
                {
                    Debug.Log("Lose weight");
                    WeightDecrease(_itemInHand.GetComponent<Collider>());
                    _itemInHand = null;
                    _isAdd = false;
                }
            }
        }*/

    }
    
    /*private void UpdateText()
    {
        weightText.text = $"{(int)weightCurrent}";
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeightIncrease(other);
            _isTrigger = true;
        }

        if (other.CompareTag("Item"))
        {
            WeightIncrease(other);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeightDecrease(other);
            _isTrigger = false;
            
            if (_itemInHand != null && _isAdd)
            {
                Debug.Log("Lose weight");
                WeightDecrease(_itemInHand.GetComponent<Collider>());
                _itemInHand = null;
                _isAdd = false;
            }
        }
        
        if (other.CompareTag("Item"))
        {
            WeightDecrease(other);
        }
        
    }

    private void WeightIncrease(Collider other)
    {
        int index = other.GetComponent<ObjectIndex>().index;
        weightCurrent += _objectDataSo.objectDatas[index].weight;
        
    }

    private void WeightDecrease(Collider other)
    {
        int index = other.GetComponent<ObjectIndex>().index;
        weightCurrent -= _objectDataSo.objectDatas[index].weight;
        
    }*/
   
}
