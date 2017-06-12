using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [Header("Time vars")]
    public float timeLimit = 300.0f; //time limit of game

	// Use this for initialization
	void Start () {
        //set up time limit from when scene starts
        timeLimit += Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckTimeLimit();
    }

    private void CheckTimeLimit()
    {
        //if time greater than time limit
        if (Time.time >= timeLimit)
        {
            //do game over stuff
        }
    }
}
