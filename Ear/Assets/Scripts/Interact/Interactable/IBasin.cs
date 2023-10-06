using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBasin : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _fullBasin;
    [SerializeField] private GameObject _almostFullBasin;
    [SerializeField] private GameObject _emptyBasin;
    private bool isFilledWater;
    private int _numFilled = 0;
    
    [SerializeField] private ObjectDataSO _objectDataSo;
    private ObjectIndex _objectIndex;
    private float _basinEmptyWeight;
    private float _waterInBucket;
    private float _maximumWaterWeight;
    private float _currentWeight;

    private void Start()
    {
        isFilledWater = false;
        _fullBasin.SetActive(false);
        _almostFullBasin.SetActive(false);
        _emptyBasin.SetActive(true);
        
        _objectIndex = GetComponent<ObjectIndex>();
        _basinEmptyWeight = _objectDataSo.objectDatas[_objectIndex.index].weight;
        _maximumWaterWeight = _objectDataSo.objectDatas[(int) NameObject.BasinFull].weight;
        _waterInBucket = _objectDataSo.objectDatas[(int) NameObject.BucketFull].weight -
                         _objectDataSo.objectDatas[(int) NameObject.BucketEmpty].weight;

        _objectDataSo.objectDatas[(int) NameObject.BasinChanging].weight = _basinEmptyWeight;
    }

    public bool Interact(Interactor interactor)
    {
        if (isFilledWater)
        {
            // ++Sound fail (pak pak)

            return false;
        }
        
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket == null)
        {
            // ++Sound fail (pak pak)

            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            _numFilled++;
            _objectIndex.ChangeIndex(NameObject.BasinChanging);
            _objectDataSo.objectDatas[_objectIndex.index].weight += _waterInBucket;

            switch (_numFilled)
            {
                case 2: // Almost
                    _fullBasin.SetActive(false);
                    _almostFullBasin.SetActive(true);
                    _emptyBasin.SetActive(false);
                    
                    SoundManager.instance.Play(SoundManager.SoundName.WaterFill);

                    break;
                
                case 3: // Full
                    isFilledWater = true;
                    _fullBasin.SetActive(true);
                    _almostFullBasin.SetActive(false);
                    _emptyBasin.SetActive(false);
                    _objectDataSo.objectDatas[_objectIndex.index].weight = _basinEmptyWeight;
                    _objectIndex.ChangeIndex(NameObject.BasinFull);
                    
                    SoundManager.instance.Play(SoundManager.SoundName.WaterFill);

                    break;
            }
            
            bucket.isFull = false;
            Debug.Log($"Complete fill water = {_numFilled} times");
            return true;
        }
        else
        {
            Debug.Log("Fill water in Bucket");
        }

        // ++Sound fail (pak pak)

        return false;
    }
}
