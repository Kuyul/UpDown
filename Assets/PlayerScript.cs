﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Declare public variables
    public float speed = 20f;

    //Declare private variables
    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);
	}
	
	// Update is called once per frame
	void Update () {
	}
}