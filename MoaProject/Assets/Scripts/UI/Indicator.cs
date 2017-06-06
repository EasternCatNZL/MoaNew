using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public float destroyDelay = 5.0f;
    [HideInInspector]
    public GameObject moa; //reference to moa object
    //public GameObject directionIndicator; //reference to direciton indicator
    [HideInInspector]
    public GameObject target; //reference to target to point at

    // Use this for initialization
    void Start()
    {
        moa = GameObject.FindGameObjectWithTag("Moa");
        target = GameObject.FindGameObjectWithTag("Nest");
        PointAtTarget();
        StartCoroutine("DestroyAfterDelay");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //point an arrow towards the target inside ui
    public void PointAtTarget()
    {
        //get angle that target is located compared to current transform
        float angle = Vector3.Angle(moa.transform.forward, target.transform.position - moa.transform.position);
        print("Angle: " + angle);
        //set arm to rotation, for circular effect
        //check if moa is on x +ve side of egg
        if (moa.transform.position.x - target.transform.position.x >= 0)
        {
            transform.Rotate(0.0f, 0.0f, angle);
        }
        //else on -ve side
        else if (moa.transform.position.x - target.transform.position.x < 0)
        {
            transform.Rotate(0.0f, 0.0f, 360 - angle);
        }
        //directionIndicator.GetComponent<FadeImage>().fadingIn = true;
    }

    IEnumerator DestroyAfterDelay()
    {
        //wait brief delay
        print("coroutine started");
        yield return new WaitForSeconds(destroyDelay);
        //destroy object
        print("couroutine returned");
        Destroy(gameObject);
    }
}
