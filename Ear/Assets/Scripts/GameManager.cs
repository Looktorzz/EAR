using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public Transform currentCheckPoint;
    public List<GameObject> colliderTriggerChangeCheckPoint;
    public int currentRoom;

    [SerializeField] private CheckPointRoomSO _checkPointSO;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }

    private void Start()
    {
        currentRoom = (int)_checkPointSO.room;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Use When ReStartGame Or ReRoom
    /*public void ClearRoomCheckPointThatBeforeThisRoom()
    {
        for (int i = 0; i < currentRoom; i++)
        {

        }
    }*/

    public void ImPlayer(GameObject playerObject)
    {
        player = playerObject;
    }

    public void ChangeCheckPoint(Transform positionCheckPoint, int numRoom)
    {
        currentCheckPoint = positionCheckPoint;
        currentRoom = numRoom;
        _checkPointSO.ChangeRoom(numRoom);
    }

    public Transform GiveMePositionReSpawn()
    {
        return currentCheckPoint;
    }

}
