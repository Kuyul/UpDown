using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour {

    public static PlatformControl Instance;

    //Declare Public GameObjects
    public GameObject[] PlatformsBottom;
    public GameObject[] PlatformsTop;
    public int NumPlatforms;
    public float LengthOffset;
    public float HeightOffset;

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
            var upDownVal = upOrDown[Random.Range(0, upOrDown.Length)]; //is the new platform going to be above the current one? or below?
            GameObject summon;
            if (upDownVal == -1){
                summon = PlatformsTop[Random.Range(0, PlatformsTop.Length)];
            }else{
                summon = PlatformsBottom[Random.Range(0, PlatformsBottom.Length)];
            }
            var platform = Instantiate(summon, newPos, Quaternion.identity); //Instantiate platform at newPos
            GeneratedPlatforms.Add(platform);
            offset += summon.GetComponent<Platform>().GetPlatformLength();
            offset += LengthOffset;

            //Up or Down
            height += upDownVal * HeightOffset;
        }
	}
	
    //Called from the ballcontroller class when it reaches the end of a platform
    //Calculates the position of where the ball should land coming from the previous platform
    public float NextPlatformBallPos(){
        platformNumber++;
        var prevPosY = GeneratedPlatforms[platformNumber-1].GetComponent<Transform>().position.y;
        var nextPosY = GeneratedPlatforms[platformNumber].GetComponent<Transform>().position.y;
        var y = nextPosY - prevPosY; //get the difference in height between two platforms
        nextPosY += (y <= 0) ? -5 : 5;//Add -5 if its below and + 5 if its above
        return nextPosY;
    }

    public float GetYDiff(){
        var prevPosY = GeneratedPlatforms[platformNumber - 1].GetComponent<Transform>().position.y;
        var nextPosY = GeneratedPlatforms[platformNumber].GetComponent<Transform>().position.y;
        var y = nextPosY - prevPosY; //get the difference in height between two platforms
        return y;
    }
}
