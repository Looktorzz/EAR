using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IDoorButSlowClose : MonoBehaviour , IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private Vector3 _position;
    bool IsOpen = false;
    float currentTime = 0f;
    float timeCoolDown = 5f;

    private void Start()
    {
        _position = transform.position;
    }

    public bool Interact(Interactor interactor)
    {
        if (!IsOpen)
        {
            transform.DOMove(new Vector3(_position.x, _position.y + 5, _position.z), 2).SetEase(Ease.InOutSine);
            Debug.Log("MOVING DOOR!");
            IsOpen = true;
            return true;
        }
        Debug.Log("WHat!?");
        return false;
    }

    private void FixedUpdate()
    {
        if (IsOpen)
        {
            if(currentTime < timeCoolDown)
            {
                currentTime += Time.fixedDeltaTime;
            }
            else
            {
                currentTime = 0f;
                IsOpen = false;
                transform.DOMove(new Vector3(_position.x, _position.y, _position.z), 2).SetEase(Ease.InOutSine);
            }

        }
    }
}
