using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour {

    public float fadeSpeed = 1.0f; //speed of one fade transition
    public float blackoutLength = 2.0f;
    //[HideInInspector]
    public bool fadingIn = false;
    //[HideInInspector]
    public bool fadingOut = false;

    private float alphaLevel; //alpha of image
    private float lastTime = 0.0f;

	// Use this for initialization
	void Start () {
        alphaLevel = 0;
        //fadingIn = true;
	}
	
	// Update is called once per frame
	void Update () {
        Fade();
	}

    private void Fade()
    {
        FadeIn();
        if(!fadingIn && fadingOut && Time.time - lastTime > blackoutLength)
        {
            FadeOut();
        }

    }

    //fades the object alpha to black
    private void FadeIn()
    {
        //get the render on object and change the alpha
        if (fadingIn)
        {
            //change the alpha relative to time
            alphaLevel += fadeSpeed * Time.deltaTime;
            //get the spriterender on plane
            GetComponent<Image>().color = new Color(0, 0, 0, alphaLevel);
            //keep values between 0 and 1
            alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
            if (alphaLevel == 1)
            {
                fadingIn = false;
                fadingOut = true;
                //this case only -> start fading out immediatly
                lastTime = Time.time;

            }
        }
    }

    //fades the object alpha to transparent
    private void FadeOut()
    {
        //get the render on object and change the alpha
        if (fadingOut)
        {
            //change the alpha relative to time
            alphaLevel -= fadeSpeed * Time.deltaTime;
            //get the spriterender on plane
            GetComponent<Image>().color = new Color(0, 0, 0, alphaLevel);
            //keep values between 0 and 1
            alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
            if (alphaLevel == 0)
            {
                fadingOut = false;
                
            }
        }
    }

    public void StartFadeIn()
    {
        fadingIn = true;
    }

    public void StartFadeOut()
    {
        fadingOut = true;
    }
}
