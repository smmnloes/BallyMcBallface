using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static I18N.Identifier;

public class UIControl : MonoBehaviour
{
    public Text livesDisplay;
    public Text scoreDisplay;
    public Text bigAnnounceDisplay;
    public Text currentPlayerDisplay;

    private RectTransform _nextLevelButton;
    private RectTransform _restartButton;
    public ParticleSystem scoreParticles;
    public ParticleSystem livesParticles;

    public int currentCheckpoint;

    GameObject _checkpointParent;
    public AudioSource gameSoundsSource;
    public AudioSource musicSource;

    public AudioClip gameOverSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;
    public AudioClip levelCompletedSound;
    public AudioClip extraLifeSound;


    private string _currentPlayer;
    private List<string> _deathText;

    private GameObject[] _toReset;

    private const int LevelRestartPenalty = -50;
    private const int LevelRestartLives = 3;
    private const int LevelCompletedBonusScore = 100;

    void Start()
    {
        _nextLevelButton = GameObject.Find("nextLevelButton").GetComponent<RectTransform>();
        _restartButton = GameObject.Find("restartButton").GetComponent<RectTransform>();
        
        GameObject.Find("NextLevelButtonText").GetComponent<Text>().text = I18N.Translate(NEXT_LEVEL);
        GameObject.Find("RestartButtonText").GetComponent<Text>().text = I18N.Translate(RESTART_LEVEL);
        
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Time.timeScale = 1; //if started from previous level 

        _toReset = GameObject.FindGameObjectsWithTag("toReset");
        _checkpointParent = GameObject.Find("Checkpoints");

        currentCheckpoint = 0;

        _currentPlayer = PlayerStats.instance.currentPlayer;

        currentPlayerDisplay.text = _currentPlayer;
        StartCoroutine(DisplayText(SceneManager.GetActiveScene().name, 3));
    }


    void Update()
    {
        
        // DEBUG INPUT
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeScore(100);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeLives(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            LevelCompleted();
        }

        //Display score & lives
        scoreDisplay.text = PlayerStats.instance.currentPlayerScore.ToString();
        livesDisplay.text = PlayerStats.instance.currentPlayerLives.ToString();
    }

    public void RestartLevel()
    {
        ChangeScore(LevelRestartPenalty);
        ChangeLives(LevelRestartLives, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void LevelCompleted()
    {
        ChangeScore(LevelCompletedBonusScore);
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            //Last level?
            bigAnnounceDisplay.text =
                $"{I18N.Translate(CONGRATULATIONS)}\n\n{I18N.Translate(GAME_COMPLETED)}\n\n" +
                $"{I18N.Translate(SCORE)}: {PlayerStats.instance.currentPlayerScore}";
        }
        else
        {
            if (PlayerStats.instance.players[_currentPlayer] <= SceneManager.GetActiveScene().buildIndex + 1)
            {
                //update max. Level in PlayerStats
                PlayerStats.instance.players[_currentPlayer] = SceneManager.GetActiveScene().buildIndex + 1;
            }

            bigAnnounceDisplay.text =
                $"{I18N.Translate(LEVEL_COMPLETED)}" +
                $"\n\n{I18N.Translate(SCORE)}: {PlayerStats.instance.currentPlayerScore}";

            Show(_nextLevelButton);
        }

        musicSource.enabled = false;
        PlayAudio(levelCompletedSound, gameSoundsSource);

        Time.timeScale = 0;
    }


    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PlayerDied()
    {
        ChangeLives(-1);

        if (PlayerStats.instance.currentPlayerLives > 0)
        {
            PlayAudio(deathSound, gameSoundsSource);
            Time.timeScale = 0.2f;
            StartCoroutine(DisplayText(I18N.Translate(OUCH), 1));
            StartCoroutine(RestartFromLastCheckpoint());
        }
        else
        {
            musicSource.enabled = false;
            PlayAudio(gameOverSound, gameSoundsSource);
            Time.timeScale = 0f;
            bigAnnounceDisplay.text = I18N.Translate(GAME_OVER);
            Show(_restartButton);
        }
    }
    
    private static void Show(RectTransform t)
    {
        t.localScale = new Vector3(1, 1, 1);
    }

    public void ChangeScore(int delta)
    {
        PlayAudio(scoreSound, gameSoundsSource);
        PlayerStats.instance.currentPlayerScore += delta;
        if (PlayerStats.instance.currentPlayerScore < 0)
        {
            PlayerStats.instance.currentPlayerScore = 0;
        }

        scoreParticles.Emit(20);
    }


    public void ChangeLives(int toChange, bool absolute = false)
    {
        if (absolute)
        {
            PlayerStats.instance.currentPlayerLives = toChange;
        }
        else
        {
            PlayerStats.instance.currentPlayerLives += toChange;

            if (toChange > 0)
                PlayAudio(extraLifeSound, gameSoundsSource);

            var particlesMain = livesParticles.main;

            particlesMain.gravityModifier = toChange < 0 ? 3 : 0;

            livesParticles.Emit(20);
        }
    }


    IEnumerator RestartFromLastCheckpoint()
    {
        yield return new WaitForSecondsRealtime(1);

        Globals.ballRigid.angularVelocity = 0; //Reset Ball's movement
        Globals.ballRigid.velocity = new Vector3(0, 0, 0);

        Globals.ball.transform.position =
            _checkpointParent.transform.GetChild(currentCheckpoint).position; //Move to current Checkpoint

        Globals.playerControl.isDead = false;

        foreach (GameObject go in _toReset)
        {
            //Reset Objects
            go.GetComponent<resetController>().Reset();
        }

        Time.timeScale = 1;
    }


    void PlayAudio(AudioClip toPlay, AudioSource source)
    {
        source.clip = toPlay;
        source.Play();
    }


    IEnumerator DisplayText(string text, int timeToDisplay)
    {
        bigAnnounceDisplay.text = text;
        yield return new WaitForSecondsRealtime(timeToDisplay);
        bigAnnounceDisplay.text = "";
    }
}