using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //Declare public variabes
    public Transform BallToFollow;

    //Declare private variables
    private float KeepDistance;
    private BallController BC;
    private float YOffsetGoingDown;
    private float YOffsetGoingUp;

	private void Start()
	{
        KeepDistance = BallToFollow.position.z - transform.position.z;
        BC = BallToFollow.GetComponent<BallController>();
        YOffsetGoingDown = transform.position.y - BallToFollow.position.y;
        YOffsetGoingUp = YOffsetGoingDown - 10;
    }

	// Update is called once per frame
	void FixedUpdate () {
                var ballPosZ = BallToFollow.position.z;
        
        if(BC.GetIsTransitioning()){
            if(BC.PassedPreviousThreshold()){
                if(BC.GetUpDown())
                    transform.position = new Vector3(transform.position.x, BallToFollow.position.y + YOffsetGoingUp, ballPosZ - KeepDistance);
                else
                    transform.position = new Vector3(transform.position.x, BallToFollow.position.y + YOffsetGoingDown, ballPosZ - KeepDistance);
            }else{
                transform.position = new Vector3(transform.position.x, transform.position.y, ballPosZ - KeepDistance);
                //YOffset = transform.position.y - BallToFollow.position.y; //Record the Yoffset to keep the ydistance between camera and ball while its flying up
            }
        }
        else
        {
            //the camera will follow the ball by keeping the same distance with the ball as it first started
            transform.position = new Vector3(transform.position.x, transform.position.y, ballPosZ - KeepDistance);
           // YOffset = transform.position.y - BallToFollow.position.y; 
        }
    }
}
