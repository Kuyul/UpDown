using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //Declare public GameObjects
    public Transform Start;
    public Transform End;

    //Declare private GameObjects
    private float PlatformLength;

    public float GetPlatformLength(){
        var diff = End.position.z - Start.position.z;
        return diff;
    }
}
