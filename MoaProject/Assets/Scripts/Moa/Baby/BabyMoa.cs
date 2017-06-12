using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMoa : MonoBehaviour {

    //Hunger vars
    [Header("Hunger Values")]
    public float maxHunger = 10.0f; //max value of hunger
    public float hungerMeter = 10.0f; //float to store hunnger of baby moa
    public float hungerDecay = 0.1f; //float to use for decaying hunger meter over time

    //animator ref
    private Animator anim;

    //particle system ref
    private ParticleSystem loveParticle;
    
	// Use this for initialization
	void Start ()
    {
        //get refs
        anim = gameObject.GetComponent<Animator>();
        loveParticle = gameObject.GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HungerDecay();
	}

    //decays hungerMeter over time
    void HungerDecay() 
    {
        //decrease hunger by set amount each frame
        hungerMeter -= hungerDecay * Time.deltaTime;
        //if hunger reaches zero, game over
        if(hungerDecay <= 0.0f)
        {
            //do game over stuff
        }
        //if hunger meter goes above max, clamp back to max
        if(hungerMeter > maxHunger)
        {
            hungerMeter = maxHunger;
        }
    }

    //used when baby moa recieves food, increases the hunger meter
    public void HungerMeterIncrease() 
    {
        //if particle exists, play
        if (loveParticle)
        {
            loveParticle.Play();
        }
        //fire anim
        anim.SetTrigger("Eat");
        //increase hunger meter
        hungerMeter++;        
    }
}
