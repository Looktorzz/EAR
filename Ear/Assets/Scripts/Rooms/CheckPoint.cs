using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawnPoint;
    private PlayerController pc;
    
    [SerializeField]private bool isClear = false;

    [SerializeField] private GameObject[] resetObject;
    
    // Start is called before the first frame update
    void Start()
    {
         pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isDead && !isClear )
        {
            player.transform.position = respawnPoint.position;
            foreach (GameObject go in resetObject)
            {
                go.GetComponent<ResetObject>().enabled = false;
                go.GetComponent<ResetObject>().enabled = true;
                
                
                Debug.Log("IsDead");

            }
        }
    }
}
