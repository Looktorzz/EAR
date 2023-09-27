using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILeakyBasin : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private GameObject _fullBasin;
    [SerializeField] private GameObject _almostFullBasin;
    [SerializeField] private GameObject _emptyBasin;
    
    [SerializeField] private ObjectDataSO _objectDataSo;
    private ObjectIndex _objectIndex;
    
    private float _waterInBucket;
    private float _maximumWaterWeight;
    private float _currentWeight;
    private float _basinEmptyWeight;
    private float speedWaterDecrease = 0.5f;
    private bool _isHaveWater = false;
    private bool _isFirstPour = true;

    private void Start()
    {
        _fullBasin.SetActive(false);
        _almostFullBasin.SetActive(false);
        _emptyBasin.SetActive(true);

        _isHaveWater = false;
        _objectIndex = GetComponent<ObjectIndex>();
        _basinEmptyWeight = _objectDataSo.objectDatas[_objectIndex.index].weight;
        _maximumWaterWeight = _objectDataSo.objectDatas[(int) NameObject.BasinFull].weight;
        _waterInBucket = _objectDataSo.objectDatas[(int) NameObject.BucketFull].weight -
                         _objectDataSo.objectDatas[(int) NameObject.BucketEmpty].weight;

        _objectDataSo.objectDatas[(int) NameObject.BasinChanging].weight = _basinEmptyWeight;
    }

    public bool Interact(Interactor interactor)
    {
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket == null)
        {
            // ++Sound fail (pak pak)
            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            _isHaveWater = true;
            _objectIndex.ChangeIndex(NameObject.BasinChanging);
            _currentWeight = _objectDataSo.objectDatas[_objectIndex.index].weight + _waterInBucket;

            if (_currentWeight >= _maximumWaterWeight)
            {
                // Full (Not use Full Sprite / Use only Changing Sprite) 
                // ++Animation water leak
                _objectDataSo.objectDatas[_objectIndex.index].weight = _maximumWaterWeight;
                _fullBasin.SetActive(true);
                _almostFullBasin.SetActive(false);
                _emptyBasin.SetActive(false);
            }
            else
            {
                // Not Full
                _objectDataSo.objectDatas[_objectIndex.index].weight += _waterInBucket;
                _fullBasin.SetActive(false);
                _almostFullBasin.SetActive(true);
                _emptyBasin.SetActive(false);
            }

            bucket.isFull = false;
            return true;
        }
        else
        {
            Debug.Log("Fill water in Bucket");
        }

        // ++Sound fail (pak pak)
        return false;
    }

    private void Update()
    {
        if (_isHaveWater)
        {
            _objectIndex.ChangeIndex(NameObject.BasinChanging);
            _objectDataSo.objectDatas[_objectIndex.index].weight -= speedWaterDecrease * Time.deltaTime;
            _currentWeight = _objectDataSo.objectDatas[_objectIndex.index].weight;

            if (_currentWeight <= _basinEmptyWeight)
            {
                // Empty
                // --Animation water leak
                _objectDataSo.objectDatas[_objectIndex.index].weight = _basinEmptyWeight;
                _objectIndex.ChangeIndex(NameObject.BasinEmpty);

                _isHaveWater = false;
                _fullBasin.SetActive(false);
                _almostFullBasin.SetActive(false);
                _emptyBasin.SetActive(true);
            }
        }
        
        Debug.Log($"_objectDataSo.objectDatas[_index].weight = {_objectDataSo.objectDatas[_objectIndex.index].weight}");
    }
}
