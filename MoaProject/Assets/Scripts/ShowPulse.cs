using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPulse : MonoBehaviour {

    public float RevealDistance = 10.0f;
    public float Distance;
    public bool Pulsing = false;
    public ParticleSystem Pulse;

    private Transform Moa;

	// Use this for initialization
	void Start () {
        Moa = GameObject.FindGameObjectWithTag("Moa").transform;
        Pulse = gameObject.GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Distance = Vector3.Distance(transform.position, Moa.position);
        if (Vector3.Distance(transform.position, Moa.position) < RevealDistance)
        {
            if(Pulse)
            {
                if(!Pulse.isPlaying)Pulse.Play();
                Pulsing = true;
            }
        }
        else
        {
            if (Pulse.isPlaying)Pulse.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Pulsing = false;
        }
	}
}
