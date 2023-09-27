using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIndex : MonoBehaviour
{
    [SerializeField] private NameObject _nameObject;
    [HideInInspector] public int index;
    
    private void Start()
    {
        index = (int) _nameObject;
    }

    public void ChangeIndex(NameObject name)
    {
        index = (int) name;
    }
}
