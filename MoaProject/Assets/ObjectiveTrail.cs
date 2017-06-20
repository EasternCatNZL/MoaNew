using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectiveTrail : MonoBehaviour {

    public NavMeshAgent Agent = null;
    public Vector3 Objective;

    static public int ObjNum = 0;

	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        print(ObjNum);
        if (ObjNum == 0) Objective = GameObject.Find("DeathSceneLocation").transform.position;
        else if (ObjNum == 1) Objective = GameObject.Find("Hub_Tree_2").transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        print(Agent.destination.ToString());
        print(Agent.remainingDistance);
        if(Agent.remainingDistance > 0.1 && Agent.remainingDistance < 1.0)
        {
            Destroy(gameObject);
        }
	}

    void LateUpdate()
    {
        Agent.SetDestination(Objective);
    }

    public void SetObjective(Vector3 _newObjective)
    {
        print(_newObjective.ToString());
        Objective = _newObjective;
    }
}
