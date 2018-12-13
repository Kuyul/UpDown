using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    //Declare public variables
    public static LevelControl Instance;
    public int LevelUpCounter = 3;
    //Declare private variables
    private int Count;
    private bool First = true;

    private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

	// Use this for initialization
	void Start()
    {
        Count = 0;
        DontDestroyOnLoad(this);
    }

    //Called from GameControl Class
    public int GetSpeedLevel(){
        var lvl = PlayerPrefs.GetInt("SpeedLevel", 1);
        if(lvl >= 7 && First)
        {
            lvl -= 2;
            PlayerPrefs.SetInt("SpeedLevel", lvl); //After gameover or restart we want the user to start at a non maximum speed;
        }
        First = false;
        return lvl;
    }

    //Called when game over
    public void AddSpeedLevel()
    {
        var level = GetSpeedLevel();
        if (level < 7)
        {
            Count++;
            if (Count >= LevelUpCounter)
            {
                PlayerPrefs.SetInt("SpeedLevel", level + 1);
                Count = 0;
            }
        }
    }

    public void SetSpeedLevel(int level)
    {
        PlayerPrefs.SetInt("SpeedLevel", level);
    }

    //Called when level = 7 and gameover;
    public void ResetSpeed()
    {
        First = true;
    }
}