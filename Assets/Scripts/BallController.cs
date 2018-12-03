﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    //Declare public variables
    public float speedUpDown;

    //Declare private variables
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();      
	}

	public void OnTriggerEnter(Collider other)
	{
        if(other.tag == "NextPattern"){
            var platform = other.transform.parent.gameObject;
            GameControl.Instance.SpawnNextPlatform(platform);
        }
	}

    public void GoUp()
    {
        rb.velocity = new Vector3(0, speedUpDown);
    }

    public void GoDown()
    {
        rb.velocity = new Vector3(0, -speedUpDown);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Platform")
        {
           rb.velocity = new Vector3(0, 0);
        }
    }
}
