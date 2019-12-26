using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance; //static reference to self

    public Dictionary<string, int> players;

    public string currentPlayer = "";

    public int currentPlayerScore;
    public int currentPlayerLives;

    private const int PlayerStartLives = 3;
    private const string SaveGameFileName = "/playerinfo.dat";

    public SystemLanguage systemLanguage;

    private static readonly SystemLanguage[] SupportedLanguages =
    {
        SystemLanguage.German,
        SystemLanguage.English
    };

    private static SystemLanguage defaultLanguage = SystemLanguage.English;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        currentPlayerLives = PlayerStartLives;
        Load();
        systemLanguage = SupportedLanguages.Contains(Application.systemLanguage)
            ? Application.systemLanguage
            : defaultLanguage;
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

    public void initPlayerLivesAndScore()
    {
        currentPlayerLives = PlayerStartLives;
        currentPlayerScore = 0;
    }
    
    private void Save()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + SaveGameFileName);

        var pdv = new PlayerDataValues {players = players, currentPlayer = currentPlayer};

        bf.Serialize(file, pdv);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + SaveGameFileName))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + SaveGameFileName, FileMode.Open);

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