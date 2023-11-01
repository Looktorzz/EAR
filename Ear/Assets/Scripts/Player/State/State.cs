using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        OnMove,
        OnDrag,
        OnHold,
        OnCrouch,
        
    }

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }

    public STATE Name;

    protected EVENT Stage;
    
    protected State NextState;
    
    protected Interactor Interactor;
    protected Item Item;
    protected Hand Hand;
    protected Rigidbody Rb;
    protected InputSystems Input;
    protected Animator Anim;
    protected SpriteRenderer SpriteRenderer;

    
    
    public State(Interactor interactor, Item item, Hand hand, Rigidbody rb, InputSystems input, Animator anim,SpriteRenderer spriteRenderer)
    {
        Interactor = interactor;
        Item = item;
        Hand = hand;
        Rb = rb;
        Input = input;
        Anim = anim;
        SpriteRenderer = spriteRenderer;
        
        Stage = EVENT.ENTER;
    }

    public virtual void Enter()
    {
        Stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        Stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        Stage = EVENT.UPDATE;
    }

    public State Process()
    {
        if (Stage == EVENT.ENTER)
        {
            Enter();
        }

        if (Stage == EVENT.UPDATE)
        {
            Update();
        }

        if (Stage == EVENT.EXIT)
        {
            Exit();
            return NextState;
        }

        return this;
    }
    

}