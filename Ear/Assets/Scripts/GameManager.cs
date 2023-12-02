using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public Transform currentCheckPoint;
    //public List<GameObject> colliderTriggerChangeCheckPoint;
    public int currentRoom;

    public bool IsFirstTime = false;
    [SerializeField] private CheckPointRoomSO _checkPointSO;
    [SerializeField] private List<ReSpawnCheckPointByArm> _checkPointObjectsList;

    [SerializeField] private SceneRoom_Storage _storage;
    
    //Don't Destory On Load
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
        if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            _storage = GameObject.FindGameObjectWithTag("Storage_Info").GetComponent<SceneRoom_Storage>();

            _checkPointObjectsList = _storage._checkPointObjectsList;
            currentRoom = (int)_checkPointSO.room;
            foreach (ReSpawnCheckPointByArm numRoom in _checkPointObjectsList)
            {
                if (currentRoom == (int)numRoom.numRoom)
                {
                    currentCheckPoint = numRoom.checkPoint;
                }
            }

            if(player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            
            player.transform.position = currentCheckPoint.position;

            if (currentRoom == 0)
            {
                player.GetComponent<PlayerController>().CutScene_ReviveStartGame();
            }
            

        }
        
    }

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

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void ReStartAllGameData()
    {
        _checkPointSO.RestartGame();
    }
}
