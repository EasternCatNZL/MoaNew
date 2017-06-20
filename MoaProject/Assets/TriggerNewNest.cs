using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewNest : MonoBehaviour {

    public int NestNumber = 0;
    public GameObject Nest = null;
    public GameObject MotherMoa = null;
    public GameObject BabyMoa = null;
    public GameObject BabyWalkingMoa = null;
    public GameObject BlackoutScreen = null;
    public GameObject GameManager = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chick")
        {
            BlackoutScreen.GetComponent<FadeImage>().StartFadeIn();

            MotherMoa.SetActive(true);
            BabyMoa.SetActive(true);

            MotherMoa.transform.position = transform.position;
            MotherMoa.transform.rotation = transform.rotation;
            BabyMoa.transform.position = Nest.transform.position;
            BabyMoa.transform.rotation = Nest.transform.rotation;
            BabyWalkingMoa.transform.position = Nest.transform.position;
            BabyWalkingMoa.transform.rotation = Nest.transform.rotation;

            BabyWalkingMoa.SetActive(false);

            GameManager.GetComponent<GameManagerScript>().BeginEndScene();
        }
    }
}
