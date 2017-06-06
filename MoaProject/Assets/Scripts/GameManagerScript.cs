using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public float timeLimit; //time limit in seconds
    float time = 0;

	// Use this for initialization
	void Start () {
        time = Time.time;
        timeLimit += Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        time = Time.time;
        if (time >= timeLimit)
        {
            //game end
        }
	}
}
