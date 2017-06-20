using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectiveTrail : MonoBehaviour {

    NavMeshAgent Agent;
    public Transform Objective;

	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Objective = GameObject.Find("DeathSceneLocation").transform;
        Agent.SetDestination(Objective.position);
    }
	
	// Update is called once per frame
	void Update () {
        print(Agent.remainingDistance);
        if(Agent.remainingDistance > 0.0 && Agent.remainingDistance < 1.0)
        {
            Destroy(gameObject);
        }
	}
}
