using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {

    //Destroy patterns as you go
	public void OnTriggerEnter(Collider other)
	{
        Debug.Log("Here");
        if(other.tag == "Platform"){
            Destroy(other.gameObject);
        }
	}
}
