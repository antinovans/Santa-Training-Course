using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandleController : MonoBehaviour
{
    public float maxTime;
    private Transform rotatePoint;
    private float timePast;

    void Awake()
    {
        maxTime = GameObject.Find("GameController").GetComponent<GameController>().timer;
        //Debug.Log("max time: " + maxTime);
        
        rotatePoint = this.gameObject.transform.GetChild(0);
        //Debug.Log("rotate point pos: " + rotatePoint.position + ", rot: " + rotatePoint.up);
    }

    void Update()
    {
        if (timePast < maxTime) {
            transform.RotateAround(rotatePoint.position, rotatePoint.up, 360/maxTime * Time.deltaTime);
            timePast += Time.deltaTime;
        }
    }
       
}
