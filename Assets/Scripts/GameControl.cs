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
    public GameObject gaLevel;
    public GameObject gaHighscore;
    public GameObject gaCurrentscore;

    public Text textLevel;
    public Text textHighscore;
    public Text textCurrentscore;
    public Text deathScore;
    public Text deathHighscore;

    private int currentScore=0;

    // Use this for initialization
    void Start () {

        Application.targetFrameRate = 300;

        if (Instance == null){
            Instance = this;
        }

        textLevel.text = "Level " + PlayerPrefs.GetInt("level", 1);
        textHighscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        textCurrentscore.text = PlayerPrefs.GetInt("currentscore", 0).ToString();
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
    }

    // Called from start screen to start game when user touches screen
    public void StartGame()
    {
        panelStart.SetActive(false);
        gaLevel.SetActive(false);
        gaHighscore.SetActive(false);
        gaCurrentscore.SetActive(true);

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
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);
    }
}
