using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerData; //static reference to self

    public Dictionary<string, int> players;

    public string currentPlayer = "";

    public int currentPlayerScore;
    public int currentPlayerLives;

    private const int PlayerStartLives = 3;
    
    void Awake()
    {
        if (playerData == null)
        {
            DontDestroyOnLoad(gameObject);
            playerData = this;
        }
        else if (playerData != this)
        {
            Destroy(gameObject);
        }
        currentPlayerLives = PlayerStartLives;
        Load(); 
    }


    void OnApplicationQuit()
    {
        Save();
    }

    //Save data (Android)
    void OnApplicationPause(bool pauseStatus)
    {
        Save();
    }


    private void Save()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/playerinfo.dat");

        var pdv = new PlayerDataValues {players = players, currentPlayer = currentPlayer};

        bf.Serialize(file, pdv);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);

            var pdv = (PlayerDataValues) bf.Deserialize(file);

            if (pdv.players != null)
            {
                players = pdv.players;
                currentPlayer = pdv.currentPlayer;
            }
            else
            {
                //if file not found or file contains null-object: Create new Dictionary
                players = new Dictionary<string, int>();
            }
        }
        else
        {
            players = new Dictionary<string, int>();
        }
    }
}

[Serializable]
class PlayerDataValues
{
    //wrapper for saving data

    public Dictionary<string, int> players;
    public string currentPlayer;
}