using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoaCall : MonoBehaviour {

    public GameObject MotherMoa = null;
    public GameObject ObjectiveTrail = null;
    public Transform Destination = null;

    private AudioSource BabyCall = null;
    private GameObject LastObjectiveTrail = null;
    private float lastTime = 0.0f;
    private float cooldown = 5.0f;

    // Use this for initialization
    void Start () {
        BabyCall = GetComponent<AudioSource>();
        lastTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.time - lastTime > cooldown && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if(BabyCall.clip)
            {
                BabyCall.Play();
            }
            GetComponent<Animator>().SetTrigger("Yell");
            LastObjectiveTrail = Instantiate(ObjectiveTrail, transform.position + transform.forward, Quaternion.identity);
            if(Destination)
            {
                LastObjectiveTrail.GetComponent<ObjectiveTrail>().SetObjective(Destination.transform.position);
            }
            lastTime = Time.time;
        }
    }

    public void SetDestination(Transform _newDestination)
    {
        Destination = _newDestination;
    }
}
