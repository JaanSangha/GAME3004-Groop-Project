using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveDataIndex
{
    public const int X = 0;
    public const int Y = 1;
    public const int Z = 2;
}

[System.Serializable]
class SaveGameData
{
    public float[] PlayerPosition;
    public float[] PlayerRotation;

    public SaveGameData() // Note: Cannot dynamically instantiate struct variables here unlike a class
    {
        PlayerPosition = new float[3]; // create empty container
        PlayerRotation = new float[3]; // create empty container
    }
}

public class SaveLoadGame : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        player = gameObject.transform;

    }
    
    void SaveGame()
    {
        // Binary formatter method of saving player position
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveGameData data = new SaveGameData();
        data.PlayerPosition[SaveDataIndex.X] = player.position.x;
        data.PlayerPosition[SaveDataIndex.Y] = player.position.y;
        data.PlayerPosition[SaveDataIndex.Z] = player.position.z;
        
        data.PlayerRotation[SaveDataIndex.X] = player.localEulerAngles.x;
        data.PlayerRotation[SaveDataIndex.Y] = player.localEulerAngles.y;
        data.PlayerRotation[SaveDataIndex.Z] = player.localEulerAngles.z;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    private void LoadGame()
    {
        // Binary Method of loading player position
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveGameData data = (SaveGameData) bf.Deserialize(file);
            file.Close();
            var x = data.PlayerPosition[SaveDataIndex.X];
            var y = data.PlayerPosition[SaveDataIndex.Y];
            var z = data.PlayerPosition[SaveDataIndex.Z];

            var rotX = data.PlayerRotation[SaveDataIndex.X];
            var rotY = data.PlayerRotation[SaveDataIndex.Y];
            var rotZ = data.PlayerRotation[SaveDataIndex.Z];

            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.rotation = Quaternion.Euler(rotX, rotY, rotZ);
            player.gameObject.GetComponent<CharacterController>().enabled = true;

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    void ResetData()
    {
        // Player Prefs Method of Resetting Data
        /* PlayerPrefs.DeleteAll();
        Debug.Log("Data reset complete"); */

        // Binary Method of Resetting Data
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
        {
            Debug.LogError("No save data to delete.");
        }
    }

    public void OnSaveButton_Pressed()
    {
        SaveGame();
    }

    public void OnLoadButton_Pressed()
    {
        LoadGame();
    }
}
