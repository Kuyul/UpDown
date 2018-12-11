using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl Instance;
    //Declare private variables
    private int Count;

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

    // Update is called once per frame
    void Update()
    {

    }

    public int GetSpeedLevel(){
        return PlayerPrefs.GetInt("SpeedLevel", 1);
    }

    public void AddSpeedLevel()
    {
        Count++;
        if (Count >= 3)
        {
            PlayerPrefs.SetInt("SpeedLevel", GetSpeedLevel() + 1);
            Count = 0;
        }
    }
}