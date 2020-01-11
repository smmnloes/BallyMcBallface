using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static I18N.Identifier;

public class MainMenuControl : MonoBehaviour
{
    private Dropdown _playerDropdown;
    private Dropdown _levelDropdown;
    private InputField _playerInputField;

    public string currentplayer;
    public int selectedLevel;
    public Text currentPlayerView;

    private RectTransform _mainPanel;
    private RectTransform _playerSelectPanel;
    private RectTransform _newPlayerPanel;
    private RectTransform _levelSelectPanel;
    private RectTransform _highScoresPanel;
    private RectTransform _donatePanel;

    private RectTransform[] _panels;

    private Text _highScorePlayersText;
    private Text _highScoreScoresText;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }


    void Start()
    {
        _playerInputField = GameObject.Find("InputPlayerField").GetComponent<InputField>();
        _levelDropdown = GameObject.Find("LevelsDropdown").GetComponent<Dropdown>();
        _playerDropdown = GameObject.Find("PlayersDropdown").GetComponent<Dropdown>();

        _mainPanel = GameObject.Find("MainPanel").GetComponent<RectTransform>();
        _playerSelectPanel = GameObject.Find("PlayerSelectPanel").GetComponent<RectTransform>();
        _newPlayerPanel = GameObject.Find("NewPlayerPanel").GetComponent<RectTransform>();
        _levelSelectPanel = GameObject.Find("LevelSelectPanel").GetComponent<RectTransform>();
        _highScoresPanel = GameObject.Find("HighscorePanel").GetComponent<RectTransform>();
        _donatePanel = GameObject.Find("DonatePanel").GetComponent<RectTransform>();
        
        _highScorePlayersText = GameObject.Find("HighScorePlayersText").GetComponent<Text>();
        _highScoreScoresText = GameObject.Find("HighScoreScoresText").GetComponent<Text>();


        _panels = new[]
        {
            _mainPanel, _levelSelectPanel, _newPlayerPanel, _playerSelectPanel, _highScoresPanel, _donatePanel
        };
        
        GameObject.Find("StartGameButtonText").GetComponent<Text>().text = I18N.Translate(START_GAME);
        GameObject.Find("SelectPlayerButtonText").GetComponent<Text>().text = I18N.Translate(SELECT_PLAYER);
        GameObject.Find("SelectLevelButtonText").GetComponent<Text>().text = I18N.Translate(SELECT_LEVEL);
        GameObject.Find("NewPlayerButtonText").GetComponent<Text>().text = I18N.Translate(NEW_PLAYER);
        GameObject.Find("DeletePlayerButtonText").GetComponent<Text>().text = I18N.Translate(DELETE_PLAYER);
        GameObject.Find("InputPlayerBackButtonText").GetComponent<Text>().text = I18N.Translate(BACK);
        GameObject.Find("InputPlayerFieldPlaceHolder").GetComponent<Text>().text = I18N.Translate(ENTER_NAME);
        GameObject.Find("HighScoresBackButtonText").GetComponent<Text>().text = I18N.Translate(BACK);
        GameObject.Find("DonateButtonText").GetComponent<Text>().text = I18N.Translate(DONATE);
        GameObject.Find("DonatePleaseText").GetComponent<Text>().text = I18N.Translate(DONATE_PLEASE);
        GameObject.Find("DonateBackButtonText").GetComponent<Text>().text = I18N.Translate(BACK);


        var quitButton = GameObject.Find("QuitButtonText");
        if (quitButton != null)
        {
            quitButton.GetComponent<Text>().text = I18N.Translate(QUIT_GAME);
        }
        
        ShowPanel(_mainPanel);
    }

    void Update()
    {
        currentplayer =
            PlayerStats.instance.GetCurrentPlayerName();
        ShowCurrentPlayerLevel();
    }

    public void ShowPanel(RectTransform t)
    {
        foreach (RectTransform panel in _panels)
        {
            if (t == panel)
            {
                Show(panel);
            }
            else
            {
                hide(panel);
            }
        }
    }

    public static void hide(RectTransform t)
    {
        t.localScale = new Vector3(0, 0, 0);
    }

    private static void Show(RectTransform t)
    {
        t.localScale = new Vector3(1, 1, 1);
    }


    public void DeletePlayer()
    {
        if (_playerDropdown.options.Count <= 0) return;
        //delete player if list has players

        var playerToDelete = _playerDropdown.options[_playerDropdown.value].text;

        _playerDropdown.options.RemoveAt(_playerDropdown.value);

        _playerDropdown.RefreshShownValue();
        _playerDropdown.value = _playerDropdown.options.Capacity; //show next in list

        PlayerStats.instance.DeletePlayer(playerToDelete);
        selectedLevel = 1;
    }


    public void StartGame()
    {
        //Start-Button
        if (PlayerStats.instance.currentPlayer == null)
        {
            ShowPanel(_newPlayerPanel);
            GameObject.Find("InputPlayerOK").GetComponent<Button>().onClick.AddListener(AddPlayerAndStartGame);
            hide(GameObject.Find("InputPlayerBack").GetComponent<RectTransform>());
        }
        else
        {
            PlayerStats.instance.InitPlayerLivesAndScore();
            SceneManager.LoadScene(selectedLevel);
        }
    }

    public void AddPlayerAndStartGame()
    {
        AddPlayer();
        currentplayer = _truncateToSizeIfBigger(_playerInputField.text, 15);
        PlayerStats.instance.SetCurrentPlayer(currentplayer);
        StartGame();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
		Application.Quit();

#endif
    }

    public void AddPlayer()
    {
        PlayerStats.instance.AddPlayer(_truncateToSizeIfBigger(_playerInputField.text, 15));

        selectedLevel = 1;
    }

    public void AcceptPlayerChoice()
    {
        //Back-Button from Playerselect
        if (_playerDropdown.options.Count > 0)
        {
            currentplayer = _playerDropdown.options[_playerDropdown.value].text;
        }
        else
        {
            currentplayer = "";
        }

        PlayerStats.instance.SetCurrentPlayer(currentplayer);
        selectedLevel = 1; //Update PlayerStats
    }

    private static string _truncateToSizeIfBigger(string s, int size)
    {
        return s.Length > size ? s.Substring(0, size) : s;
    }

    private void ShowCurrentPlayerLevel()
    {
        if (currentPlayerView != null)
        {
            //show current player and selected level
            if (PlayerStats.instance.currentPlayer == null)
                currentPlayerView.text = I18N.Translate(CHOOSE_PLAYER_FIRST);
            else
                currentPlayerView.text = $"{I18N.Translate(PLAYER)}: {currentplayer}\nLEVEL: {selectedLevel}";
        }
    }

    public void PopulateLevelDropdown()
    {
        _levelDropdown.ClearOptions();

        if (PlayerStats.instance.currentPlayer != null)
        {
            //add possible levels for player to dropdown
            int levelReached = PlayerStats.instance.currentPlayer.maxLevel;
            for (int i = 1; i <= levelReached; i++)
            {
                _levelDropdown.options.Add(new Dropdown.OptionData() {text = "Level " + i});
            }

            _levelDropdown.RefreshShownValue();
        }
        else
        {
            _levelDropdown.options.Add(new Dropdown.OptionData() {text = I18N.Translate(CHOOSE_PLAYER_FIRST)});
            _levelDropdown.RefreshShownValue();
        }

        _levelDropdown.value = 0;
    }

    public void PopulatePlayerDropdown()
    {
        _playerDropdown.ClearOptions();

        foreach (var player in PlayerStats.instance.players)
        {
            //get all players from Dictionary and put in dropdown
            _playerDropdown.options.Add(new Dropdown.OptionData() {text = player.name});
        }

        _playerDropdown.value = _playerDropdown.options.Capacity; //show last player created
    }

    public void ChangeSelectedLevel()
    {
        _levelDropdown.RefreshShownValue();
        selectedLevel = _levelDropdown.value + 1;
    }

    public void PopulateHighScoreBoard()
    {
        _highScorePlayersText.text = "";
        _highScoreScoresText.text = "";

        var players = PlayerStats.instance.players;
        players.Sort((player1, player2) => player2.highScore.CompareTo(player1.highScore));

        foreach (var player in players)
        {
            if (player.highScore > 0)
            {
                _highScorePlayersText.text += player.name + "\n";
                _highScoreScoresText.text += player.highScore + "\n";
            }
        }
    }
}