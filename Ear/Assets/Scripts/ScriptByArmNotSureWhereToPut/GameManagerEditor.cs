using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager manager = (GameManager)target;
        if(GUILayout.Button("Reset Value Where Room"))
        {
           manager.ReStartAllGameData();
            Debug.Log("Reset Data Finished");
        }
    }


}
