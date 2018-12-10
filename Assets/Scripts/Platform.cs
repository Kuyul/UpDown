using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //Declare public GameObjects
    public Transform Start;
    public Transform End;
    //Declare its properties
    public GameObject Upper;
    public GameObject Below;
    public GameObject[] OUppers;
    public GameObject[] OBelows;

    //Declare private GameObjects
    private float PlatformLength;

    public float GetPlatformLength(){
        var diff = End.position.z - Start.position.z;
        return diff;
    }

    //Called from the Platformcontroller class to change material properties based on its level
    public void SetMaterials(Material upper, Material below, Material oUpper, Material oBelow)
    {
        Upper.GetComponent<MeshRenderer>().material = upper;
        Below.GetComponent<MeshRenderer>().material = below;
        for(int i = 0; i < OUppers.Length; i++)
        {
            OUppers[i].GetComponent<MeshRenderer>().material = oUpper;
        }

        for(int i = 0; i < OBelows.Length; i++)
        {
            OBelows[i].GetComponent<MeshRenderer>().material = oBelow;
        }
    }
}
