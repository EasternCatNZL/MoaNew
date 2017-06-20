using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoaCall : MonoBehaviour {

    public GameObject MotherMoa = null;
    public GameObject ObjectiveTrail = null;

    private float lastTime = 0.0f;
    private float cooldown = 5.0f;

    // Use this for initialization
    void Start () {
        lastTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.time - lastTime > cooldown && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            GetComponent<Animator>().SetTrigger("Yell");
            Instantiate(ObjectiveTrail, transform.position + transform.forward, Quaternion.identity);
            lastTime = Time.time;
        }
    }
}
