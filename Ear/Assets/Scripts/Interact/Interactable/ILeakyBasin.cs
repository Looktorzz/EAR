using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILeakyBasin : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField] private GameObject _fullBasin;
    [SerializeField] private GameObject _twoFillBasin;
    [SerializeField] private GameObject _oneFillBasin;
    [SerializeField] private GameObject _emptyBasin;
    
    [SerializeField] private ObjectDataSO _objectDataSo;
    private ObjectIndex _objectIndex;
    
    private float _waterInBucket;
    private float _maximumWaterWeight;
    private float _currentWeight;
    private float _basinEmptyWeight;
    private float speedWaterDecrease = 0.15f;
    private bool _isHaveWater = false;

    [Header("WaterLeaky")]
    [SerializeField] private GameObject _audio;
    [SerializeField] private ParticleSystem _waterParticle;

    private void Start()
    {
        _fullBasin.SetActive(false);
        _twoFillBasin.SetActive(false);
        _oneFillBasin.SetActive(false);
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
            SoundManager.instance.Play(SoundManager.SoundName.WaterFill);
            _objectIndex.ChangeIndex(NameObject.BasinChanging);
            _currentWeight = _objectDataSo.objectDatas[_objectIndex.index].weight + _waterInBucket;
            
            if (_currentWeight >= _maximumWaterWeight)
            {
                // Full
                _objectDataSo.objectDatas[_objectIndex.index].weight = _maximumWaterWeight;
            }
            else
            {
                _objectDataSo.objectDatas[_objectIndex.index].weight += _waterInBucket;
            }
            
            bucket.BucketIsFull(false);
            return true;
        }
        else
        {
            Debug.Log("Fill water in Bucket");
        }

        // ++Sound fail (pak pak)
        return false;
    }

    private bool _isCheckWater = false;
    
    private void CheckWater(bool isHaveWater)
    {
        if (_isCheckWater == !isHaveWater)
        {
            if (isHaveWater)
            {
                Debug.Log("Water Particle Play");
                _waterParticle.Play();
            }
            else
            {
                Debug.Log("Water Particle Stop");
                _waterParticle.Stop();
            }
        }

        _isCheckWater = isHaveWater;
    }

    private void Update()
    {
        _audio.SetActive(_isHaveWater);

        if (_isHaveWater)
        {
            CheckWater(true);
            
            _objectIndex.ChangeIndex(NameObject.BasinChanging);
            _objectDataSo.objectDatas[_objectIndex.index].weight -= speedWaterDecrease * Time.deltaTime;
            _currentWeight = _objectDataSo.objectDatas[_objectIndex.index].weight;

            if (_currentWeight >= _basinEmptyWeight + 4)
            {
                // 3
                _fullBasin.SetActive(true);
                _twoFillBasin.SetActive(false);
                _oneFillBasin.SetActive(false);
                _emptyBasin.SetActive(false);
            }
            else if (_currentWeight >= _basinEmptyWeight + 2)
            {
                // 2
                _fullBasin.SetActive(false);
                _twoFillBasin.SetActive(true);
                _oneFillBasin.SetActive(false);
                _emptyBasin.SetActive(false);
            }
            else if (_currentWeight >= _basinEmptyWeight)
            {
                // 1
                _fullBasin.SetActive(false);
                _twoFillBasin.SetActive(false);
                _oneFillBasin.SetActive(true);
                _emptyBasin.SetActive(false);
            }
            else
            {
                // Empty
                _fullBasin.SetActive(false);
                _twoFillBasin.SetActive(false);
                _oneFillBasin.SetActive(false);
                _emptyBasin.SetActive(true);
                
                _objectDataSo.objectDatas[_objectIndex.index].weight = _basinEmptyWeight;
                _objectIndex.ChangeIndex(NameObject.BasinEmpty);
                _isHaveWater = false;
            }
        }
        else
        {
            CheckWater(false);
        }
    }
}
