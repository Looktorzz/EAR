using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBasin : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    
    [SerializeField] private GameObject _fullBasin;
    [SerializeField] private GameObject _twoFillBasin;
    [SerializeField] private GameObject _oneFillBasin;
    [SerializeField] private GameObject _emptyBasin;
    private int _numFilled = 0;
    
    [SerializeField] private ObjectDataSO _objectDataSo;
    private ObjectIndex _objectIndex;
    private float _basinEmptyWeight;
    private float _waterInBucket;
    private float _maximumWaterWeight;
    private float _currentWeight;

    private void Start()
    {
        _fullBasin.SetActive(false);
        _twoFillBasin.SetActive(false);
        _oneFillBasin.SetActive(false);
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
        Bucket bucket = interactor.GetComponentInChildren<Bucket>();

        if (bucket == null)
        {
            // ++Sound fail (pak pak)
            SoundManager.instance.Play(SoundManager.SoundName.Fail);
            Debug.Log("You don't have Bucket");
            return false;
        }

        if (bucket.isFull)
        {
            if (_numFilled <= 2)
            {
                SoundManager.instance.Play(SoundManager.SoundName.WaterFill);
                

                _numFilled++;
                _objectIndex.ChangeIndex(NameObject.BasinChanging);
                _objectDataSo.objectDatas[_objectIndex.index].weight += _waterInBucket;
                bucket.BucketIsFull(false);
                
                CheckFill();
                Debug.Log($"Complete fill water = {_numFilled} times");
                return true;
            }

            // ++Sound fail (pak pak)
            SoundManager.instance.Play(SoundManager.SoundName.Fail);
            return false;
            
        }
        else
        {
            if (_numFilled >= 1)
            {
                SoundManager.instance.Play(SoundManager.SoundName.WaterFill);
                
                _objectDataSo.objectDatas[_objectIndex.index].weight -= _waterInBucket;
                _numFilled--;
                bucket.BucketIsFull(true);

                CheckFill();
                Debug.Log($"Complete get water = {_numFilled} times");
                return true;
            }
            
            // ++Sound fail (pak pak)
            SoundManager.instance.Play(SoundManager.SoundName.Fail);
            return false;
        }
    }

    private void CheckFill()
    {
        switch (_numFilled)
        {
            case 0:
                _fullBasin.SetActive(false);
                _twoFillBasin.SetActive(false);
                _oneFillBasin.SetActive(false);
                _emptyBasin.SetActive(true);
                    
                break;
            
            case 1:
                StartCoroutine(CheckFillWithSound());
                break;
                
            case 2: // Almost
                StartCoroutine(CheckFillWithSound());
                break;
                
            case 3: // Full
                StartCoroutine(CheckFillWithSound());
                break;
        }
    }
    
    IEnumerator CheckFillWithSound()
    {
        yield return StartCoroutine(SoundWaterFill());
        
        switch (_numFilled)
        {
            case 1:
                _fullBasin.SetActive(false);
                _twoFillBasin.SetActive(false);
                _oneFillBasin.SetActive(true);
                _emptyBasin.SetActive(false);
                    
                break;
                
            case 2: // Almost

                _fullBasin.SetActive(false);
                _twoFillBasin.SetActive(true);
                _oneFillBasin.SetActive(false);
                _emptyBasin.SetActive(false);

                break;
                
            case 3: // Full

                _fullBasin.SetActive(true);
                _twoFillBasin.SetActive(false);
                _oneFillBasin.SetActive(false);
                _emptyBasin.SetActive(false);
                _objectDataSo.objectDatas[_objectIndex.index].weight = _basinEmptyWeight;
                _objectIndex.ChangeIndex(NameObject.BasinFull);

                break;
        }

    }
    
        IEnumerator SoundWaterFill()
    {
        SoundManager.instance.Play(SoundManager.SoundName.WaterFill);
        yield return new WaitForSeconds(.6f);
        

    }
}
