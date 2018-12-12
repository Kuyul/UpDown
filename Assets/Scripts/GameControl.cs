﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl Instance;

    //Declare controllers
    public PlatformControl PlatformControl;
    public BallController BallController;
    public Transform Ball;

    public GameObject panelStart;
    public GameObject panelDeath;
    public GameObject panelTop;
    public GameObject panelLevelClear;
    public GameObject gaLevel;
    public GameObject gaHighscore;
    public GameObject peDeath;

    public Animator textMoveUp;
    public Animator cameraMove;

    public Text textLevel;
    public Text textHighscore;
    public Text textCurrentscore;
    public Text deathScore;
    public Text deathHighscore;
    public Text levClearLevel;
    public Text levClearCurrentscore;
    public Text levClearHighscore;
    public Text textCurrentLev;
    public Text textNextLev;
    public Slider ProgressBar;

    private int currentScore = 0;

	private void Awake()
	{
        var level = LevelControl.Instance.GetSpeedLevel();
        var speed = (level + 1) * 15;
        BallController.speed = speed;
        BallController.speedUpDown = speed / 12 * 8;
        PlatformControl.HeightOffset = speed;
        PlatformControl.LengthOffset = speed * 2;
        BallController.heightChangeSpeed = speed * 2.5f;
        PlatformControl.NumPlatforms = (level - 1) / 2 + 2;
        Debug.Log("Level is: " + level);
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

        textCurrentLev.text = PlayerPrefs.GetInt("level", 1).ToString();
        textNextLev.text = (PlayerPrefs.GetInt("level", 1)+1).ToString();
    }

    private void Update()
    {
        var value = Ball.position.z/PlatformControl.GetTotalLength();
        ProgressBar.value = value;
    }

    public void UpDown()
    {
        BallController.UpDown();
    }

    //Called from Ballcontroller to end game when obstacle is touched
    public void GameOver()
    {
        BallController.gameObject.SetActive(false);
        LevelControl.Instance.ResetSpeed();
        Instantiate(peDeath, BallController.gameObject.transform.position, Quaternion.identity);
        StartCoroutine(Delay(1f));
    }

    IEnumerator Delay(float time)
    {

        yield return new WaitForSeconds(time);
        panelTop.SetActive(false);
        panelDeath.SetActive(true);
        deathScore.text = currentScore.ToString();
        deathHighscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        PlayerPrefs.SetInt("continuescore", 0);
    }

    // Called from start screen to start game when user touches screen
    public void StartGame()
    {
        cameraMove.SetTrigger("cameramove");
        panelStart.SetActive(false);
        gaLevel.SetActive(false);
        gaHighscore.SetActive(false);
        textMoveUp.SetTrigger("start");
        StartCoroutine(Delay2(0.25f));
    }

    IEnumerator Delay2(float time)
    {      
        yield return new WaitForSeconds(time);
        Destroy(cameraMove);
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

    public void ResetPlayerPrefs(){
        PlayerPrefs.DeleteAll();
    }
}
