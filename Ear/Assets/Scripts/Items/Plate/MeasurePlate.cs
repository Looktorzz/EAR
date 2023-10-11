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
    [SerializeField] private TextMeshProUGUI weightTextOne;
    [SerializeField] private TextMeshProUGUI weightTextTwo;
    public float _weightCurrent = 0;
    public float getWeightCurrent => _weightCurrent;
    
    [SerializeField] private GameObject _closeMonitor;
    [SerializeField] private GameObject _openMonitor;
    [SerializeField] private GameObject _plateNotTrigger;
    [SerializeField] private GameObject _plateTrigger;

    [SerializeField] private GameObject _boxPhysic;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Collider[] _colliders;
    private GameObject _itemInHand;
    
    private void Start()
    {
        _closeMonitor.SetActive(true);
        _openMonitor.SetActive(false);
        
        _plateNotTrigger.SetActive(true);
        _plateTrigger.SetActive(false);
    }

    private void FoundCollider()
    {
        _colliders = Physics.OverlapBox(_boxPhysic.transform.position, _boxPhysic.transform.localScale/2,
            Quaternion.identity, _layerMask);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_boxPhysic.transform.position, _boxPhysic.transform.localScale);
    }
    
    private void PlusWeight()
    {
        float weight = 0;

        for (int i = 0; i < _colliders.Length; i++)
        {
            int index;
            
            if (_colliders[i].TryGetComponent<Item>(out Item itemInPlayer))
            {
                if (itemInPlayer.itemInHand != null)
                {
                    index = itemInPlayer.itemInHand.GetComponent<ObjectIndex>().index;
                    weight += _objectDataSo.objectDatas[index].weight;
                }
            }
            
            index = _colliders[i].GetComponent<ObjectIndex>().index;
            weight += _objectDataSo.objectDatas[index].weight;
        }

        _weightCurrent = weight;
        CheckOnOffMonitor(_weightCurrent);
        
        Array.Clear(_colliders,0,_colliders.Length);
    }

    private void CheckOnOffMonitor(float num)
    {
        if (num > 0)
        {
            _closeMonitor.SetActive(false);
            _openMonitor.SetActive(true);
            
            _plateNotTrigger.SetActive(false);
            _plateTrigger.SetActive(true);
        }
        else
        {
            _closeMonitor.SetActive(true);
            _openMonitor.SetActive(false);
            
            _plateNotTrigger.SetActive(true);
            _plateTrigger.SetActive(false);
        }
    }

    private void Update()
    {
        FoundCollider();
        if (_colliders != null)
        {
            PlusWeight();
            weightTextOne.text = ((int) _weightCurrent).ToString("0");
            weightTextTwo.text = ((int) _weightCurrent).ToString("0");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            // ++Sound trigger
            SoundManager.instance.Play(SoundManager.SoundName.PressSwitch);
        }
    }
}
