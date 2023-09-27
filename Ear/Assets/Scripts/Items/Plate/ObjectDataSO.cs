using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectData", menuName = "ScriptableObject/ObjectDataSO", order = 1)]
public class ObjectDataSO : ScriptableObject
{
    public List<ObjectData> objectDatas;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField] private string name;
    [field: SerializeField] private NameObject nameObject;
    [field: SerializeField] public float weight;
}

public enum NameObject
{
    Player,
    BucketEmpty,
    BucketFull,
    BasinEmpty,
    BasinFull,
    BasinChanging,
}