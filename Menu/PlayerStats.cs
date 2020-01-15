using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance; //static reference to self

    public List<PlayerInfo> players;

    public PlayerInfo currentPlayer;

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

    public void InitPlayerLivesAndScore()
    {
        currentPlayerLives = PlayerStartLives;
        currentPlayerScore = 0;
    }

    private void Save()
    {
        var bf = new BinaryFormatter();
        using (var file = File.Create(Application.persistentDataPath + SaveGameFileName))
        {
            var pdv = new PlayerPersistency {players = players, currentPlayer = currentPlayer};
            bf.Serialize(file, pdv);
        }
    }

    private void Load()
    {
        print(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + SaveGameFileName))

        {
            using (var file = File.Open(Application.persistentDataPath + SaveGameFileName, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                var pdv = (PlayerPersistency) bf.Deserialize(file);

                if (pdv.players != null)
                {
                    players = pdv.players;
                    currentPlayer = pdv.currentPlayer;
                }
                else
                {
                    //if file not found or file contains null-object: Create new Dictionary
                    players = new List<PlayerInfo>();
                }
            }
        }
        else
        {
            players = new List<PlayerInfo>();
        }
    }

    public void UpdateCurrentPlayerMaxLevel(int newMaxLevel)
    {
        if (currentPlayer.maxLevel <= newMaxLevel)
        {
            currentPlayer.maxLevel = newMaxLevel;
        }
    }

    public void UpdateCurrentPlayerHighScore()
    {
        if (currentPlayer.highScore < currentPlayerScore)
        {
            currentPlayer.highScore = currentPlayerScore;
        }
    }

    public string GetCurrentPlayerName()
    {
        return currentPlayer == null ? "" : currentPlayer.name;
    }

    public void AddPlayer(string name)
    {
        if (name != "" && !_playerExists(name))
        {
            players.Add(new PlayerInfo() {highScore = 0, maxLevel = 1, name = name});
        } //add player to Dictionary
    }

    public void DeletePlayer(string name)
    {
        players.Remove(_getPlayerByName(name));
    }

    public void SetCurrentPlayer(string name)
    {
        currentPlayer = name == "" ? null : _getPlayerByName(name);
    }

    private PlayerInfo _getPlayerByName(string name)
    {
        foreach (var player in players)
        {
            if (player.name == name)
            {
                return player;
            }
        }

        return null;
    }

    private bool _playerExists(string name)
    {
        foreach (var player in players)
        {
            if (player.name == name)
            {
                return true;
            }
        }

        return false;
    }

    [Serializable]
    class PlayerPersistency
    {
        public List<PlayerInfo> players;
        public PlayerInfo currentPlayer;
    }

    [Serializable]
    public class PlayerInfo
    {
        public string name;
        public int maxLevel;
        public int highScore;
    }
}