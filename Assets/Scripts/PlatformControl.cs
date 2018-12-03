using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour {

    //Declare Public GameObjects
    //Declare platform prefabs
    public GameObject[] Platforms;
    public int NumPlatforms = 4;

	// Use this for initialization
	void Start () {
        var offset = 0f; //Offset to Z axis
		for(int i = 0; i < NumPlatforms; i++)
        {
            var newPos = new Vector3(0, 0, offset);
            var summon = Platforms[Random.Range(0, Platforms.Length)];
            Instantiate(summon, newPos, Quaternion.identity); //Instantiate platform at newPos
            offset += summon.GetComponent<Platform>().GetPlatformLength();
            offset += 50;
        }
	}
	
}
