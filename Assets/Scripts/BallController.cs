using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    //Declare public variables
    public float speed;

    //Declare private variables
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * speed;
	}

	public void OnTriggerEnter(Collider other)
	{
        if(other.tag == "NextPattern"){
            var platform = other.transform.parent.gameObject;
            GameControl.Instance.SpawnNextPlatform(platform);
        }
	}
}
