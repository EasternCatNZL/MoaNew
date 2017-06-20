using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoaFood : MonoBehaviour
{
    //Inventory variables
    public float numStones = 0.0f; //number of stones currently stored
    public float numBerries = 0.0f; //number of berries currently stored

    //Inventory control variables
    [Header("Inventory Control")]
    public float stoneDecay = 0.1f; //float to use for decaying stones over time, public so it can be edited in unity

    //Animator
    private Animator Anim; //ref to animator

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //CanEatBerries();
        StoneDecay();
    }

    //Checks to see if moa currently has stones
    //true if moa has stones
    bool CanEatBerries() 
    {
        //checks if can eat a berry
        bool canEat = false;
        //check number of stones
        if (numStones > 0.0f)
        {
            canEat = true;
        }
        return canEat;
    }

    //decays stones eaten over time
    void StoneDecay() 
    {
        //if currently holding any number of stones
        if (numStones > 0.0f)
        {
            //every second, decrease stones value by set amount
            numStones -= stoneDecay * Time.deltaTime;
            //if stones value becomes less than or equal to, can no longer eat
            if(numStones <= 0.0f)
            {
                numStones = 0;
            }
        }

    }

    //compares colleder with gameobject tag to do actions
    void OnTriggerStay(Collider other) 
    {
        //if colliding with stone, if input pressed, eat stone
        if(/*other.gameObject.CompareTag("Stone") && Input.GetKeyDown(KeyCode.E) ||*/ other.gameObject.CompareTag("Stone") && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            //set stone to false
            other.gameObject.GetComponent<ShowPulse>().DeactivateSelf();
            //add one stone to inventory
            numStones++;
            //fire anim
            if (Anim)
            {
                Anim.SetTrigger("Stone");
            }
            //print("ate stone");       
        }
        //if colliding with berry, eat only if possible
        else if(/*other.gameObject.CompareTag("Berry") && Input.GetKeyDown(KeyCode.E) || */other.gameObject.CompareTag("Berry") && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            //check if berries can be eaten
            if (CanEatBerries())
            {
                //set berry to false
                other.gameObject.GetComponent<ShowPulse>().DeactivateSelf();
                //increase number of berries in inventory
                numBerries++;
                //fire anim
                if (Anim)
                {
                    Anim.SetTrigger("Eat");
                }
                //print("ate berry");
            }
            //else
            //{

            //}
        }

        else if(/*other.gameObject.CompareTag("Nest") && Input.GetKeyDown(KeyCode.E) || */other.gameObject.CompareTag("Chick") && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            //check if currently holding berries
            if(numBerries > 0.0f)
            {
                //decrease berries by one
                numBerries--;
                //increase baby's hunger
                other.gameObject.GetComponent<BabyMoa>().HungerMeterIncrease();
                //fire anim
                if (Anim)
                {
                    Anim.SetTrigger("Feed");
                }
                //print("fed berry");              
            }
            //else
            //{

            //}
            
        }
    }
}
