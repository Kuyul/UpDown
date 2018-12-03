using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl Instance;

    //Declare controllers
    public PlatformControl PlatformControl;
    public BallController BallController;

	// Use this for initialization
	void Start () {

        Application.targetFrameRate = 300;

        if (Instance == null){
            Instance = this;
        }
	}

    public void UpDown()
    {
        BallController.UpDown();
    }

    //Called from Ballcontroller to spawn next pattern
    public void SpawnNextPlatform(GameObject pattern){
        PlatformControl.SpawnNextPlatform(pattern);
    }

    //Called from Ballcontroller to end game when obstacle is touched
    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
