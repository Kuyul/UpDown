using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl Instance;

    //Declare controllers
    public PlatformControl PlatformControl;
    public BallController BallController;

    public GameObject panelStart;
    public GameObject panelDeath;
    public GameObject panelTop;
    public GameObject panelLevelClear;
    public GameObject gaLevel;
    public GameObject gaHighscore;

    public Animator textMoveUp;

    public Text textLevel;
    public Text textHighscore;
    public Text textCurrentscore;
    public Text deathScore;
    public Text deathHighscore;
    public Text levClearLevel;
    public Text levClearCurrentscore;
    public Text levClearHighscore;

    private int currentScore = 0;

	private void Awake()
	{
        var level = LevelControl.Instance.GetSpeedLevel();
        var speed = (level + 1) * 15;
        BallController.speed = speed;
        PlatformControl.HeightOffset = speed;
        PlatformControl.LengthOffset = speed * 2;
        BallController.heightChangeSpeed = speed * 3;
        PlatformControl.NumPlatforms = (level - 1) / 2 + 2;
        Debug.Log(speed);
        Debug.Log(level);
	}

	// Use this for initialization
	void Start () {

        Application.targetFrameRate = 300;

        if (Instance == null){
            Instance = this;
        }

        textLevel.text = "Level " + PlayerPrefs.GetInt("level", 1);
        textHighscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        if (PlayerPrefs.GetInt("continuescore", 0) == 0)
        {
            textCurrentscore.text = PlayerPrefs.GetInt("currentscore", 0).ToString();
        }
        else
        {
            textCurrentscore.text = PlayerPrefs.GetInt("continuescore", 0).ToString();
            currentScore = PlayerPrefs.GetInt("continuescore", 0);
        }
	}

    public void UpDown()
    {
        BallController.UpDown();
    }

    //Called from Ballcontroller to end game when obstacle is touched
    public void GameOver()
    {
        BallController.gameObject.SetActive(false);
        panelTop.SetActive(false);
        panelDeath.SetActive(true);
        deathScore.text = currentScore.ToString();
        deathHighscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        PlayerPrefs.SetInt("continuescore", 0);
    }

    // Called from start screen to start game when user touches screen
    public void StartGame()
    {
        panelStart.SetActive(false);
        gaLevel.SetActive(false);
        gaHighscore.SetActive(false);

        textMoveUp.SetTrigger("start");
        BallController.StartBallMovement();
    }

    // Called from Deathbutton after death
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void IncrementScore()
    {
        currentScore = currentScore + PlayerPrefs.GetInt("level", 1);
        textCurrentscore.text = currentScore.ToString();

        if (currentScore > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
    }

    // Increments level by 1 when level is complete
    public void LevelComplete()
    {
        BallController.gameObject.GetComponent<MeshRenderer>().enabled = false;
        BallController.enabled = false;
        levClearLevel.text = "level " + PlayerPrefs.GetInt("level", 1);
        levClearCurrentscore.text = currentScore.ToString();
        levClearHighscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);

        PlayerPrefs.SetInt("continuescore", currentScore); // score to start from when you complete level
        LevelControl.Instance.AddSpeedLevel();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("continuescore", 0);
    }
}
