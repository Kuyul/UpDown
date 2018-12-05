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
            var step = Time.deltaTime * heightChangeSpeed;
            //NewPos is calculated from MovePlayer where it retreives the appropriate balues from Platformcontrol
            transform.position = Vector3.MoveTowards(transform.position, NewPos, step);
            if(transform.position == NewPos){
                Moving = false;
                rb.velocity = new Vector3(0, 0, speed);
                Touch.SetActive(true);
            }
        }
	}

    //Moves the player gameobject to different platform position
	public void MovePlayer(){
        if (!Moving)
        {
            var z = PlatformControl.Instance.WidthOffset;
            var y = PlatformControl.Instance.GetNextPlatformY();
            NewPos = new Vector3(transform.position.x, y, transform.position.z + z);
            Moving = true;
            rb.velocity = new Vector3(0, 0, 0);
            //if new position is lower, then activate fall and vice versa.
            //We don't want the user to be able to click up down while in air, so we'll disable touch
            Touch.SetActive(false);
            if(transform.position.y >= NewPos.y){
                Ball.SetUp(false); //Ball fall down
            }else{
                Ball.SetUp(true); //Ball rise up
            }
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
