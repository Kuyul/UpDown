using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour ,IPointerDownHandler{

    public BallController ball;

    private bool updown;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!updown)
        {
            ball.GoUp();
            updown = true;
        }
        else
        {
            ball.GoDown();
            updown = false;
        }
    }

}
