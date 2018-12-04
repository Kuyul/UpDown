using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Declare public variables
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
            }
        }
	}

    //Moves the player gameobject to different platform position
	public void MovePlayer(){
        if (!Moving)
        {
            var z = PlatformControl.Instance.LengthOffset;
            var y = PlatformControl.Instance.GetNextPlatformY();
            NewPos = new Vector3(transform.position.x, y, transform.position.z + z);
            Moving = true;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Whenever the ball reaches the end of a platform go up or down
        if (other.tag == "End")
        {
            Debug.Log("Here");
            MovePlayer();
        }
    }
}
