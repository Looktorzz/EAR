using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    [SerializeField] private SaveData _saveData = new SaveData();
    
    [ContextMenu("Save")]
    private void Save()
    {
        var binaryFormatter = new BinaryFormatter();

        FileStream file = File.Create($"{Application.persistentDataPath}/save.dap");
        
        binaryFormatter.Serialize(file,_saveData);
        file.Close();
    }

    [ContextMenu("Load")]
    private void Load()
    {
        if (!File.Exists($"{Application.persistentDataPath}/save.dap"))
        {
            return;
        }

        var binaryFormatter = new BinaryFormatter();

        FileStream file = File.Open($"{Application.persistentDataPath}/save.dap", FileMode.Open);

        _saveData = (SaveData) binaryFormatter.Deserialize(file);
        file.Close();
    }
}

[Serializable]
public class SaveData
{
    [SerializeField] private float[] position;
    
}

public interface ISaveable
{
    object CaptureState();
    void RestoreState(object state);
}
