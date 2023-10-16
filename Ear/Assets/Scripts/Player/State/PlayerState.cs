using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveVector2;
    
    private Interactor _interactor;
    private Item _item;
    private Hand _hand;
    private Rigidbody _rb;
    private InputSystems _input;
    private Animator _animator;

    private State _currentState;

    private void Awake()
    {
        _input = new InputSystems();
        _rb = GetComponent<Rigidbody>();
        _interactor = GetComponent<Interactor>();
        _item = GetComponent<Item>();
        _hand = GetComponent<Hand>();

        _currentState = new OnMove(_interactor,_item,_hand,_rb,_input,_animator,_spriteRenderer);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}


