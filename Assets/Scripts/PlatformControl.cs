using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour {

    public static PlatformControl Instance;

    //Declare Public GameObjects
    public GameObject[] Platforms;
    public int NumPlatforms = 4;
    public float WidthOffset = 50f;
    public float HeightOffset = 50f;

    //Declare private variables
    private int platformNumber = 0;
    private List<GameObject> GeneratedPlatforms = new List<GameObject>();

	private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
	}

	// Use this for initialization
	void Start () {
        var height = 0f;
        int[] upOrDown = { -1, 1 };
        var offset = 0f; //Offset to Z axis
        //Generate n number of platforms at once when the level begins
		for(int i = 0; i < NumPlatforms; i++)
        {
            var newPos = new Vector3(0, height, offset);
            var summon = Platforms[Random.Range(0, Platforms.Length)];
            var platform = Instantiate(summon, newPos, Quaternion.identity); //Instantiate platform at newPos
            GeneratedPlatforms.Add(platform);
            offset += summon.GetComponent<Platform>().GetPlatformLength();
            offset += WidthOffset;

            //Up or Down
            height += upOrDown[Random.Range(0,upOrDown.Length)] * HeightOffset;
        }
	}
	
    //Called from the ballcontroller class when it reaches the end of a platform
    public float GetNextPlatformY(){
        platformNumber++;
        return GeneratedPlatforms[platformNumber].GetComponent<Transform>().position.y;
    }
}
