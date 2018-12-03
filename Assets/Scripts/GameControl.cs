using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static GameControl Instance;

    //Declare controllers
    public PlatformControl PlatformControl;

	// Use this for initialization
	void Start () {

        Application.targetFrameRate = 300;

        if (Instance == null){
            Instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called from Ballcontroller to spawn next pattern
    public void SpawnNextPlatform(GameObject pattern){
        PlatformControl.SpawnNextPlatform(pattern);
    }
}
