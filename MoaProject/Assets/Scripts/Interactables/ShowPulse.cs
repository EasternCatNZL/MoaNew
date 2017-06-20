using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPulse : MonoBehaviour
{

    //Distance vars
    public float revealDistance = 10.0f; //distance where object can be revealed within
    public float veryCloseDistance = 3.0f; //distance where hint can appear

    [Header("Show Hint Script")]
    public ShowHint showHint; //ref to show hint script on hint image

    //particle vars
    [Header("Particles")]
    public ParticleSystem pulse; //ref to particle effect

    //control vars
    private bool isPulsing = false; //checks to see if should be pulsing
    private bool isClose = false; //checks to see if moa is close
    private bool isVeryClose = false; //checks to see if moa is very close by
    private bool approachTimeSet = false; //checks to see if time when moa approached has been set

    //timer control vars
    [Header("Ui timer vars")]
    public float timeToShowHint = 3.0f; //time that needs to past before ui hint is shown

    private float enterTime; //time moa approached the thing

    //ref to moa
    private Transform Moa; //ref to moa object

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Moa"))
            Moa = GameObject.FindGameObjectWithTag("Moa").transform;
        pulse = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckProximity();
        if (isClose)
        {
            SetApproachTime();
            ProximityPulse();
        }

    }

    private void ProximityPulse()
    {
        //if particle exists
        if (pulse)
        {
            //if particle currently not playing, than play
            if (!pulse.isPlaying)
            {
                pulse.Play();
            }
            //set pulsing to true
            isPulsing = true;
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

    //check if moa is close
    private void CheckProximity()
    {
        //if moa is close
        if (Vector3.Distance(transform.position, Moa.position) <= revealDistance)
        {
            isClose = true;
            //debug if
            if (showHint)
            {
                //if moa is very close
                if (Vector3.Distance(transform.position, Moa.position) <= veryCloseDistance)
                {
                    print("Moa very close");
                    showHint.isShowing = true;
                }
                else
                {
                    showHint.notShowing = true;
                }
            }

        }
        else
        {
            isClose = false;
        }
    }

    //check if approach time set, if not set
    private void SetApproachTime()
    {
        //if not set
        if (!approachTimeSet)
        {
            //set the tim
            enterTime = Time.time;
            //change is set to true
            approachTimeSet = true;
        }
    }

}
