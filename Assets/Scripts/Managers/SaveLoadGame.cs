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
    [SerializeField]
    List<Transform> allTransforms;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Gets rigidbodies of all gameObjects in the scene
        Rigidbody[] allRb = GameObject.FindObjectsOfType<Rigidbody>(true);

        foreach(Rigidbody rb in allRb)
        {
            allTransforms.Add(rb.gameObject.transform);
        }
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

        List<SaveGameData> datas = new List<SaveGameData>();
        foreach(Transform tr in allTransforms)
        {
            var newData = new SaveGameData();
            newData.PlayerPosition[SaveDataIndex.X] = tr.position.x;
            newData.PlayerPosition[SaveDataIndex.Y] = tr.position.y;
            newData.PlayerPosition[SaveDataIndex.Z] = tr.position.z;

            newData.PlayerRotation[SaveDataIndex.X] = tr.localEulerAngles.x;
            newData.PlayerRotation[SaveDataIndex.Y] = tr.localEulerAngles.y;
            newData.PlayerRotation[SaveDataIndex.Z] = tr.localEulerAngles.z;

            datas.Add(newData);
        }
        bf.Serialize(file, datas);
        //bf.Serialize(file, data);
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