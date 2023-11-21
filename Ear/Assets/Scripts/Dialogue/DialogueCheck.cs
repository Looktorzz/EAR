using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCheck : MonoBehaviour
{
    public bool IsStart = false;
    public bool IsEnding = false;

    DialogueManager dialogueManager;
    public GameObject _gameobject;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (IsStart)
        {
            if (dialogueManager.IsFinish)
            {
                if (_gameobject != null)
                {
                    _gameobject.SetActive(true);
                    this.gameObject.SetActive(false);
                }

                
            }
        }
        
    }

    public void STARTDialogue()
    {
        IsStart = true;
    }
}
