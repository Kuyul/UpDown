using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    //Declare public variables
    public float speedUpDown;

    //Declare private variables
    private Rigidbody rb;
    private bool Up = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();      
	}

    private void Update()
    {
        if (Up)
        {
            GoUp();
        }
        else
        {
            GoDown();
        }
    }

    public void GoUp()
    {
        var newPos = new Vector3(0, 4, 0);
        var step = speedUpDown * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, step);
    }

    public void GoDown()
    {
        var newPos = new Vector3(0, -4, 0);
        var step = speedUpDown * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, step);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextPattern")
        {
            var platform = other.transform.parent.gameObject;
            GameControl.Instance.SpawnNextPlatform(platform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Platform")
        {
           rb.velocity = new Vector3(0, 0);
        }

        if (collision.collider.tag == "Obstacle")
        {
            GameControl.Instance.GameOver();
        }
    }

    public void UpDown()
    {
        Up = !Up;
    }
}
