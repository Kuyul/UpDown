using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Declare public variables
    public GameObject Touch;
    public BallController Ball;
    public float speed = 20f;
    public float heightChangeSpeed = 30f;

    //Declare private variables
    private Rigidbody rb;
    private Vector3 NewPos;
    private bool Moving = false;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);
	}

	private void Update()
	{
        if (Moving)
        {
            GameControl.Instance.pePlatformJump.SetActive(true);
            var step = Time.deltaTime * heightChangeSpeed;
            //NewPos is calculated from MovePlayer where it retreives the appropriate values from Platformcontrol
            transform.position = Vector3.MoveTowards(transform.position, NewPos, step);
            if(transform.position == NewPos){
                GameControl.Instance.pePlatformJump.SetActive(false);
                Moving = false;
                rb.velocity = new Vector3(0, 0, speed);
                Touch.SetActive(true);
            }
        }
	}

    //Moves the player gameobject to different platform position
	


}
