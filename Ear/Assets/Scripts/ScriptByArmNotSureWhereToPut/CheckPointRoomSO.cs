using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataCheckPointWhichRoom", menuName = "CheckPointRooomScriptableObjects")]
public class CheckPointRoomSO : ScriptableObject
{
    public Level level = Level.Level1_Tutorial;
    public Room room = Room.Room_Start;

    public void RestartGame()
    {
        level = Level.Level1_Tutorial;
        room = Room.Room_Start;
    }



    public void ChangeLevel(int numLevel)
    {
        switch (numLevel)
        {
            case 1:
                level = Level.Level1_Tutorial;
                break;
            case 2:
                level = Level.Level2_Main;
                break;
            case 3:
                level = Level.Level3_BeforeEnd;
                break;
            case 4:
                level = Level.Level4_End;
                break;
            default:
                Debug.LogError("They're no that Level here.");
            break;
        }
    }

    public void ChangeRoom(int numRoom)
    {
        // tips : click minus on the left to make it shot switch
        switch (numRoom)
        {
            case 0:
                room = Room.Room_Start;
                break;
            case 100:
                room = Room.Room1;
                break;
            case 101:
                room = Room.Room1_1;
                break;
            case 102:
                room = Room.Room1_2;
                break;
            case 103:
                room = Room.Room1_3;
                break;
            case 200:
                room = Room.Room2;
                break;
            case 201:
                room = Room.Room2_1;
                break;
            case 202:
                room = Room.Room2_2;
                break;
            case 203:
                room = Room.Room2_3;
                break;
            case 300:
                room = Room.Room3;
                break;
            case 301:
                room = Room.Room3_1;
                break;
            case 302:
                room = Room.Room3_2;
                break;
            case 303:
                room = Room.Room3_3;
                break;
            case 400:
                room = Room.Room4;
                break;
            case 401:
                room = Room.Room4_1;
                break;
            case 402:
                room = Room.Room4_2;
                break;
            case 403:
                room = Room.Room4_3;
                break;
            case 500:
                room = Room.Room5;
                break;
            case 501:
                room = Room.Room5_1;
                break;
            case 502:
                room = Room.Room5_2;
                break;
            case 503:
                room = Room.Room5_3;
                break;
            case 600:
                room = Room.Room6;
                break;
            case 601:
                room = Room.Room6_1;
                break;
            case 602:
                room = Room.Room6_2;
                break;
            case 603:
                room = Room.Room6_3;
                break;
            case 700:
                room = Room.Room7;
                break;
            case 701:
                room = Room.Room7_1;
                break;
            case 702:
                room = Room.Room7_2;
                break;
            case 703:
                room = Room.Room7_3;
                break;
            case 800:
                room = Room.Room8;
                break;
            case 801:
                room = Room.Room8_1;
                break;
            case 802:
                room = Room.Room8_2;
                break;
            case 803:
                room = Room.Room8_3;
                break;
            case 900:
                room = Room.Room9;
                break;
            case 901:
                room = Room.Room9_1;
                break;
            case 902:
                room = Room.Room9_2;
                break;
            case 903:
                room = Room.Room9_3;
                break;

            default :
                Debug.LogError("Umm They're no room in list, need to add more");
                break;
        }
    }

}

public enum Level
{
    Level1_Tutorial = 1,
    Level2_Main = 2,
    Level3_BeforeEnd = 3,
    Level4_End = 4,
}

public enum Room 
{
    Room_Start = 0,
    Room1 = 100,
    Room1_1 = 101,
    Room1_2 = 102,
    Room1_3 = 103,
    Room2 = 200,
    Room2_1 = 201,
    Room2_2 = 202,
    Room2_3 = 203,
    Room3 = 300,
    Room3_1 = 301,
    Room3_2 = 302,
    Room3_3 = 303,
    Room4 = 400,
    Room4_1 = 401,
    Room4_2 = 402,
    Room4_3 = 403,
    Room5 = 500,
    Room5_1 = 501,
    Room5_2 = 502,
    Room5_3 = 503,
    Room6 = 600,
    Room6_1 = 601,
    Room6_2 = 602,
    Room6_3 = 603,
    Room7 = 700,
    Room7_1 = 701,
    Room7_2 = 702,
    Room7_3 = 703,
    Room8 = 800,
    Room8_1 = 801,
    Room8_2 = 802,
    Room8_3 = 803,
    Room9 = 900,
    Room9_1 = 901,
    Room9_2 = 902,
    Room9_3 = 903,

}
