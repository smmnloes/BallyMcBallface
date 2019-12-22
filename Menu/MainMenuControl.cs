using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuControl : MonoBehaviour
{
    public Dropdown playerDropdown;
    public Dropdown levelDropdown;
    public InputField playerInputField;

    public string currentplayer;
    public int selectedLevel;
    public Text currentPlayerView;


    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Update()
    {
        currentplayer = PlayerStats.instance.currentPlayer; //Get current player from PlayerStats and refresh display
        showCurrentPlayerLevel();
    }


    public void deletePlayer()
    {
        if (playerDropdown.options.Count > 0)
        {
            //delete player if list has players

            string playertodelete = playerDropdown.options[playerDropdown.value].text;

            playerDropdown.options.RemoveAt(playerDropdown.value); //delete from dropdown

            playerDropdown.RefreshShownValue();
            playerDropdown.value = playerDropdown.options.Capacity; //show next in list

            PlayerStats.instance.players.Remove(playertodelete);
            selectedLevel = 1; //delete from Dictionary
        }
    }


    public void StartGame()
    {
        //Start-Button
        if (currentplayer != "")
        {
            PlayerStats.instance.currentPlayerLives = 3;
            PlayerStats.instance.currentPlayerScore = 0;
            SceneManager.LoadScene(selectedLevel);
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
		Application.Quit();

#endif
    }

    public void addPlayer()
    {
        if (playerInputField.text != "" && !PlayerStats.instance.players.ContainsKey(playerInputField.text.ToUpper()))
        {
            PlayerStats.instance.players.Add(playerInputField.text.ToUpper(), 1);
        } //add player to Dictionary

        selectedLevel = 1;
    }

    public void acceptPlayerChoice()
    {
        //Back-Button from Playerselect
        if (playerDropdown.options.Count > 0)
        {
            currentplayer = playerDropdown.options[playerDropdown.value].text;
        }
        else
        {
            currentplayer = "";
        }

        PlayerStats.instance.currentPlayer = currentplayer;
        selectedLevel = 1; //Update PlayerStats
    }


    public void showCurrentPlayerLevel()
    {
        if (currentPlayerView != null)
        {
            //show current player and selected level
            if (currentplayer == "" || currentplayer == null)
                currentPlayerView.text = "PLEASE SELECT PLAYER";
            else
                currentPlayerView.text = "PLAYER: " + currentplayer + "\nLEVEL: " + selectedLevel;
        }
    }

    public void populateLevelDropdown()
    {
        levelDropdown.ClearOptions();

        if (currentplayer != "")
        {
            //add possible levels for player to dropdown
            int levelReached = PlayerStats.instance.players[currentplayer];
            for (int i = 1; i <= levelReached; i++)
            {
                levelDropdown.options.Add(new Dropdown.OptionData() {text = "LEVEL " + (i)});
            }

            levelDropdown.RefreshShownValue();
        }
        else
        {
            levelDropdown.options.Add(new Dropdown.OptionData() {text = "CHOOSE PLAYER FIRST!"}); //no player chosen
            levelDropdown.RefreshShownValue();
        }

        levelDropdown.value = 0;
    }

    public void populatePlayerDropdown()
    {
        playerDropdown.ClearOptions();

        foreach (string currentKey in PlayerStats.instance.players.Keys)
        {
            //get all players from Dictionary and put in dropdown
            playerDropdown.options.Add(new Dropdown.OptionData() {text = currentKey});
        }

        playerDropdown.value = playerDropdown.options.Capacity; //show last player created
    }

    public void changeSelectedLevel()
    {
        levelDropdown.RefreshShownValue();
        selectedLevel = levelDropdown.value + 1;
    }
}