﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDistance : MonoBehaviour {

    //Object reference vars
    [Header("Target object")]
    public GameObject otherObject; //reference to other object


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //calculates the distance between self and other
    public float GetDistanceBetween()
    {
        //initialise float
        float distance = 0;
        //if other exists
        if (otherObject)
        {
            //calculate the distance between
            distance = Vector3.Distance(otherObject.transform.position, transform.position);
        }
        //return the distance
        return distance;
    }
}
