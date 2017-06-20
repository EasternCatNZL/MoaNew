using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEagle : MonoBehaviour {

    public GameObject Eagle = null;
    public ParticleSystem FeatherDrop = null;
    public GameObject Light = null;
    public GameObject BabyMoa = null;
    public GameObject BabyMoaArrow = null;
    public GameObject HubTree2Nav = null;

    public GameObject MoaCallObject = null;
    private AudioSource EagleCall = null;
    private int State = 1; //From which nest is the baby moa coming from
    private float ObjectiveShowDelay = 0.0f;
	// Use this for initialization
	void Start () {
        EagleCall = GetComponent<AudioSource>();
        if (FeatherDrop.isPlaying)
        {
            FeatherDrop.Pause();
        }

        Light.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chick")
        {
            if(EagleCall.clip)
            {
                EagleCall.Play();
            }
            Eagle.GetComponent<EagleFlyBy>().ReleaseTheEagle();
            if(!FeatherDrop.isPlaying)
            {
                FeatherDrop.Play();
            }
            Light.SetActive(true);
            if(State == 1)
            {
                ObjectiveTrail.ObjNum = 1;
            }
            ObjectiveShowDelay = Time.time;
            BabyMoaArrow.SetActive(true);
            BabyMoaArrow.GetComponent<ObjectiveArrow>().Objective = HubTree2Nav.transform;
        }
    }
}
