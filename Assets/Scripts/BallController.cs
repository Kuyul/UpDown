using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    //Declare public variables
    public float speedUpDown;
    public float speed = 20f;
    public float heightChangeSpeed = 60f;
    public float slowMoSpeed = 5f;
    public GameObject Touch;
    public GameObject[] ParticleEffects;
    public GameObject FireTrail;

    //Declare private variables
    private float OriginalChangeSpeed;
    private int ActivatePE = 0;
    private bool Up = false;
    private Rigidbody rb;
    private Vector3 NewPos;
    private float Offset = 0f;
    private float PreviousThreshold = 0f;
    private bool IsTransitioning = false;
    private bool UpDownTransitioning = false; //Flag updown transitioning

    void Start(){
        rb = GetComponent<Rigidbody>();

        OriginalChangeSpeed = heightChangeSpeed;
    }

    private void FixedUpdate()
    {
        //Different moving mechanism depending on whether the ball is moving platforms or not
        if (IsTransitioning)
        {
            ChangePlatforms();
        }
        else if (UpDownTransitioning)
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
            SetIsTransitioning(false);
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

        //Check if ball transition is complete
        if(transform.position == newPos){
            UpDownTransitioning = false;
            //Reached Top if Up = true
            if(Up){
                ReachedTop();
            }else{
                ReachedBottom();
            }
        }

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
            JumpPlatforms();
        }
    }

	public void UpDown()
    {
        Up = !Up;
        UpDownTransitioning = true; //Flag
        //Starting jump or fall
        if(Up){
            JumpUp();
        }else{
            JumpDown();
        }
    }

    //Moves the player gameobject to different platform position
    public void JumpPlatforms()
    {
        var z = PlatformControl.Instance.LengthOffset +3f;
        var y = PlatformControl.Instance.NextPlatformBallPos();
        var diff = PlatformControl.Instance.GetYDiff();
        Up = (diff >= 0) ? true : false;
        PreviousThreshold = Offset;
        Offset += diff;
        NewPos = new Vector3(transform.position.x, y, transform.position.z + z);
        SetIsTransitioning(true);
        //if new position is lower, then activate fall and vice versa.
        //We don't want the user to be able to click up down while in air, so we'll disable touch
        StartCoroutine(StartSlowMo());
    }

    IEnumerator StartSlowMo(){
        heightChangeSpeed = slowMoSpeed;
        yield return new WaitForSeconds(0.3f);
        heightChangeSpeed = OriginalChangeSpeed;
    }

    //Enable and disable and set platform transition variables
    private void SetIsTransitioning(bool move){
        if(move){
            FireTrail.SetActive(true);
            rb.velocity = new Vector3(0, 0, 0);
            if (Up == true)
            {
                JumpUp();
            }
            else
            {
                JumpDown();
            }

        }else{
            FireTrail.SetActive(false);
            //Activate Particle effects
            if (ActivatePE < ParticleEffects.Length)
            {
                ParticleEffects[ActivatePE].SetActive(true);
                ActivatePE += 1;
            }
            rb.velocity = new Vector3(0, 0, speed);
            if(Up){
                ReachedTop();
            }else{
                ReachedBottom();
            }
        }
        IsTransitioning = move;
        Touch.SetActive(!move);
    }

    //Called from the camera script
    //Check if upper or bottom platform is passed during platform move
    public bool PassedPreviousThreshold(){
        if (transform.position.y > PreviousThreshold + 5 || transform.position.y < PreviousThreshold - 5)
        {
            return true;
        }
        else {
            return false;
        };
    }

    //Called from the camera script
    public bool GetIsTransitioning(){
        return IsTransitioning;
    }

    public void JumpUp(){


    }

    public void JumpDown(){


    }

    public void ReachedTop(){
        GameControl.Instance.IncrementScore();
    }

    public void ReachedBottom(){
        GameControl.Instance.IncrementScore();
    }

    public void StartBallMovement()
    {
        rb.velocity = new Vector3(0, 0, speed);
    }
}
