using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour
{

    //image ref
    private SpriteRenderer sprite; //ref to sprite renderer

    //camera ref
    [Header("Camera")]
    public GameObject mainCamera; //ref to camera

    //control vars
    [Header("Control vars")]
    public float fadeSpeed = 4.0f; //speed at which card fades away
    public float hintDelay = 5.0f; //time that needs to past before hint is shown
    [HideInInspector]
    public float waitStartTime; //time to start waiting on delay from
    [HideInInspector]
    public bool hasApproached = false; //checks to see if moa has approached the object

    //image control
    [HideInInspector]
    public bool isShowing = false; //checks if image is fading from transparent to color
    [HideInInspector]
    public bool notShowing = false; //checks if image is fading from color to transparent

    private float alphaLevel = 0.0f; //current alpha level of the card


    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        mainCamera = GameObject.Find("MainCamera");
        //in case any were not transparent to begin with
        sprite.color = new Color(1, 1, 1, alphaLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing && Time.time >= waitStartTime + hintDelay)
        {
            ShowImage();

            //always look towards camera
            transform.LookAt(mainCamera.transform);
        }
        if (notShowing)
        {
            HideImage();
        }
    }

    //show the image
    private void ShowImage()
    {
        //change alpha level relative to time
        alphaLevel += fadeSpeed * Time.deltaTime;
        //change image alpha based on alpha level
        sprite.color = new Color(1, 1, 1, alphaLevel);
        //keep values between 0 and 1
        alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
        if (alphaLevel >= 1)
        {
            isShowing = false;
        }
    }

    //hide the image
    private void HideImage()
    {
        //change alpha level relative to time
        alphaLevel -= fadeSpeed * Time.deltaTime;
        //change image alpha based on alpha level
        sprite.color = new Color(1, 1, 1, alphaLevel);
        //keep values between 0 and 1
        alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
        if (alphaLevel <= 0)
        {
            notShowing = false;
        }
    }

    //set up approach
    public void Approach()
    {
        hasApproached = true;
        waitStartTime = Time.time;
    }

    //set up move away
    public void MovedAway()
    {
        hasApproached = false;
        isShowing = false;
    }
}