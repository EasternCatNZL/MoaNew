using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertFarAway : MonoBehaviour {

    public float threshold = 5; //distance that player can travel out before being alerted
    public float farThreshold = 10; //distance that begins constant alert
    public float farAlertDelay = 3; //delay in alert when in far circle
    public CalculateDistance calcDistance; //reference to calculate distance script
    //public Indicator indicator; //reference to indicator script
    public GameObject indicatorArm; //reference to indicator arm object

    bool withinInnerRing = false; //checks if player is within inner ring area
    bool inOuterRing = false; //checks if player is in outer ring
    float distanceLastTime; //distance before last update
    float lastAlertTime; //time since last alert

	// Use this for initialization
	void Start () {
        lastAlertTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        CheckArea();
	}

    void CheckArea()
    {
        //check if currently in inner area
        if (calcDistance.GetDistanceBetween() <= threshold)
        {
            //check if last check was outside the area
            if(distanceLastTime > threshold)
            {
                //change in inner ring back to true
                withinInnerRing = true;
                print("Entering inner area");
            }
        }
        //check if in middle area
        else if (calcDistance.GetDistanceBetween() > threshold && calcDistance.GetDistanceBetween() <= farThreshold)
        {
            //check if last check was inside the area
            if(distanceLastTime <= threshold)
            {
                //change in inner ring to false
                withinInnerRing = false;
                //trigger visual cue
                //indicator.PointAtTarget();
                SpawnIndicator();

                print("Leaving inner area");
            }
            //check if last check was in outer ring
            else if(distanceLastTime > farThreshold)
            {
                //change in outer ring to false
                inOuterRing = false;
                print("Leaving outer area");
            }
        }
        //check if in outer area
        else if (calcDistance.GetDistanceBetween() > farThreshold)
        {
            //check if last check was in middle area
            if (distanceLastTime <= farThreshold && distanceLastTime > threshold)
            {
                //change to in outer ring
                inOuterRing = true;
                //trigger visual cue
                print("Entering outer ring");
            }
            //when in outer ring, constantly alert player
            if (Time.time - lastAlertTime >= farAlertDelay)
            {
                SpawnIndicator();
                lastAlertTime = Time.time;
            }
        }
        distanceLastTime = calcDistance.GetDistanceBetween();
    }

    //spawns the canvas object that has directional indicator
    void SpawnIndicator()
    {
        GameObject armClone = Instantiate(indicatorArm, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        print("Spawn Indicator");
        armClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
