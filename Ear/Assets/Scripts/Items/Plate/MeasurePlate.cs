using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
    
    private void Start()
    {
        UpdateText();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        
    }

    private void Update()
    {
        UpdateText();

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
        }
        
    }
    
    private void UpdateText()
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
        weightCurrent += _objectDataSo.objectDatas[index].weight;;
        
    }

    private void WeightDecrease(Collider other)
    {
        int index = other.GetComponent<ObjectIndex>().index;
        weightCurrent -= _objectDataSo.objectDatas[index].weight;;
        
    }
   
}
