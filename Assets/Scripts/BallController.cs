using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    //Declare public variables
    public float speedUpDown;
    public PlayerScript Player;

    //Declare private variables

    private bool Up = false;

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
        var newPos = new Vector3(0, 5, 0);
        var step = speedUpDown * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, step);
    }

    public void GoDown()
    {
        var newPos = new Vector3(0, -5, 0);
        var step = speedUpDown * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            GameControl.Instance.GameOver();
        }
    }

	public void UpDown()
    {
        Up = !Up;
    }

	public void SetUp(bool val)
	{
        Up = val;
	}
}
