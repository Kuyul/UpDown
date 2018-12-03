using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour {

    //Declare Public GameObjects
    //Declare platform prefabs
    public GameObject Platform;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Spawn Next Pattern on the same level
    public void SpawnNextPlatform(GameObject platform){
        var control = platform.GetComponent<Platform>();
        var length = control.GetPlatformLength();

        var nextPosZ = platform.transform.position.z + length + 10;
        var nextPos = new Vector3(0,0,nextPosZ);
        Instantiate(Platform, nextPos, Quaternion.identity);
    }
}
