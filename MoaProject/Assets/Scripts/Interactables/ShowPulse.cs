using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPulse : MonoBehaviour {

    //Distance vars
    public float revealDistance = 10.0f; //distance where object can be revealed within

    //particle vars
    [Header("Particles")]
    public ParticleSystem pulse; //ref to particle effect

    private bool isPulsing = false; //checks to see if should be pulsing
    
    //ref to moa
    private Transform Moa; //ref to moa object

	// Use this for initialization
	void Start () {
        Moa = GameObject.FindGameObjectWithTag("Moa").transform;
        pulse = gameObject.GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //if distance between moa and object is less than reveal distance, pulse
        if (Vector3.Distance(transform.position, Moa.position) <= revealDistance)
        {
            //if particle exists
            if(pulse)
            {
                //if particle currently not playing, than play
                if (!pulse.isPlaying)
                {
                    pulse.Play();
                }
                //set pulsing to true
                isPulsing = true;
            }
        }
        //else if distance becomes greater than reveal distance
        else
        {
            //turn off pulse
            if (pulse.isPlaying)
            {
                pulse.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            //set pulsing to false
            isPulsing = false;
        }
	}
}
