using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour ,IPointerDownHandler{

    public BallController ball;

    public void OnPointerDown(PointerEventData eventData)
    {
        ball.UpDown();
    }

}
