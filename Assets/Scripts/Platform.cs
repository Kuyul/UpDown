using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //Declare public GameObjects
    public GameObject Upper;
    public GameObject Below;

    //Declare private GameObjects
    private float PlatformLength;

	// Use this for initialization
	void Start () {
        PlatformLength = Upper.GetComponent<Transform>().localScale.z;
	}
	
	
    public float GetPlatformLength(){
        return PlatformLength;
    }
}
