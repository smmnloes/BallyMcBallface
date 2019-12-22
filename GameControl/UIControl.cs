using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Text livesDisplay;
    public Text scoreDisplay;
    public Text bigAnnounceDisplay;
    public Text currentPlayerDisplay;

    public GameObject nextLevelButton;
    public GameObject restartButton;
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


    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Time.timeScale = 1; //if started from previous level 

        _toReset = GameObject.FindGameObjectsWithTag("toReset");
        _checkpointParent = GameObject.Find("Checkpoints");

        currentCheckpoint = 0;

        _currentPlayer = PlayerStats.playerData.currentPlayer;

        currentPlayerDisplay.text = _currentPlayer;
        StartCoroutine(DisplayText(SceneManager.GetActiveScene().name.ToUpper(), 3));
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Go to menu on Key
            SceneManager.LoadScene(0);
        }


        if (Input.GetKeyDown(KeyCode.S)) //DEBUG
            IncreaseScore(1);

        if (Input.GetKey(KeyCode.Space)) RestartScene();

        if (Input.GetKeyDown(KeyCode.L))
            ChangeLives(1);

        if (Input.GetKeyDown(KeyCode.X))
            LevelCompleted();
        //DEBUG


        scoreDisplay.text = PlayerStats.playerData.currentPlayerScore.ToString(); //Display score & lives
        livesDisplay.text = PlayerStats.playerData.currentPlayerLives.ToString();
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void LevelCompleted()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            //Last level?
            bigAnnounceDisplay.text = "CONGRATULATIONS!\n\nGAME COMPLETED!";
        }
        else
        {
            if (PlayerStats.playerData.players[_currentPlayer] <= SceneManager.GetActiveScene().buildIndex + 1)
            {
                //update max. Level in PlayerStats
                PlayerStats.playerData.players[_currentPlayer] = SceneManager.GetActiveScene().buildIndex + 1;
            }

            bigAnnounceDisplay.text = "LEVEL COMPLETED\n\nSCORE: " + PlayerStats.playerData.currentPlayerScore;
            nextLevelButton.SetActive(true);
        }

        musicSource.enabled = false;
        PlayAudio(levelCompletedSound, gameSoundsSource);

        Time.timeScale = 0;
    }


    public void LoadNextLevel()
    {
        //called by nextLevelButton

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PlayerDied()
    {
        ChangeLives(-1);

        if (PlayerStats.playerData.currentPlayerLives > 0)
        {
            PlayAudio(deathSound, gameSoundsSource);
            Time.timeScale = 0.2f;
            StartCoroutine(DisplayText("OUCH", 1));
            StartCoroutine(RestartFromLastCheckpoint()); //Move to Checkpoint
        }
        else
        {
            musicSource.enabled = false;

            PlayAudio(gameOverSound, gameSoundsSource);
            Time.timeScale = 0f; //Game over
            bigAnnounceDisplay.text = "GAME OVER";

            restartButton.SetActive(true);
        }
    }


    public void IncreaseScore(int toIncrease)
    {
        PlayAudio(scoreSound, gameSoundsSource);
        PlayerStats.playerData.currentPlayerScore += toIncrease;
        scoreParticles.Emit(20);
    }


    public void ChangeLives(int toChange)
    {
        PlayerStats.playerData.currentPlayerLives += toChange;

        if (toChange > 0)
            PlayAudio(extraLifeSound, gameSoundsSource);

        var particlesMain = livesParticles.main;

        particlesMain.gravityModifier = toChange < 0 ? 3 : 0;

        livesParticles.Emit(20);
    }


    IEnumerator RestartFromLastCheckpoint()
    {
        yield return new WaitForSecondsRealtime(1);

        var ballRigid = Globals.ball.GetComponent<Rigidbody2D>();
        ballRigid.angularVelocity = 0; //Reset Ball's movement
        ballRigid.velocity = new Vector3(0, 0, 0);

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