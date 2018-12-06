using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    //Declare public variables
    public float speedUpDown;
    public float speed = 20f;
    public float heightChangeSpeed = 5f;
    public GameObject Touch;

    //Declare private variables
    private bool Up = false;
    private Rigidbody rb;
    private Vector3 NewPos;
    private float Offset = 0f;
    private float PreviousThreshold = 0f;
    private bool MovingPlatforms = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);
    }

    private void FixedUpdate()
    {
        //Different moving mechanism depending on whether the ball is moving platforms or not
        if (MovingPlatforms)
        {
            ChangePlatforms();
        }
        else
        {
            if (Up)
            {
                GoUpOrDown(true);
            }
            else
            {
                GoUpOrDown(false);
            }
        }
    }

    //Method called from the update method while changing platforms
    public void ChangePlatforms(){
        var step = heightChangeSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, NewPos, step);
        //If ball transition is complete, set Movingplatforms bool to false and continue moving forward at normal speed
        if(transform.position == NewPos){
            SetMovingPlatforms(false);
        }
    }

    //Method called from the update method when mouse button is clicked to move the ball up or down
    public void GoUpOrDown(bool Up){
        //Check whether the ball is moving up or down
        Vector3 newPos;
        if (Up){
            newPos = new Vector3(0, Offset + 5, transform.position.z);
        }else{
            newPos = new Vector3(0, Offset - 5, transform.position.z);
        }

        //While moving platforms we want a slow motion effect
        var step = speedUpDown * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, newPos, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            GameControl.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Whenever the ball reaches the end of a platform go up or down
        if (other.tag == "End")
        {
            Debug.Log("End of platform");
            MovePlayer();
        }
    }

	public void UpDown()
    {
        Up = !Up;
    }

    //Moves the player gameobject to different platform position
    public void MovePlayer()
    {
        var z = PlatformControl.Instance.LengthOffset;
        var y = PlatformControl.Instance.NextPlatformBallPos();
        var diff = PlatformControl.Instance.GetYDiff();
        Up = (diff >= 0) ? true : false;
        PreviousThreshold = Offset;
        Offset += diff;
        NewPos = new Vector3(transform.position.x, y, transform.position.z + z);
        SetMovingPlatforms(true);
        //if new position is lower, then activate fall and vice versa.
        //We don't want the user to be able to click up down while in air, so we'll disable touch
            
    }

    private void SetMovingPlatforms(bool move){
        if(move){
            rb.velocity = new Vector3(0, 0, 0);
        }else{
            rb.velocity = new Vector3(0, 0, speed);
        }
        MovingPlatforms = move;
        Touch.SetActive(!move);
    }

    public bool PassedPreviousThreshold(){
        if (transform.position.y > PreviousThreshold + 5 || transform.position.y < PreviousThreshold - 5)
        {
            return true;
        }
        else {
            return false;
        };
    }

    public bool GetMovingPlatforms(){
        return MovingPlatforms;
    }
}
