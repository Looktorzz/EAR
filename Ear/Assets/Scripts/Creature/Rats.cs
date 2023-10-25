using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rats : MonoBehaviour
{
    [SerializeField] private GameObject rat;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Run()
    {
        rat.transform.DOMoveX(transform.position.x - 2, 2);
    }
}
