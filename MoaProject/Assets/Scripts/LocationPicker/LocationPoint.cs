﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocationPoint : MonoBehaviour {

    [Header("ImageRef")]
    //public Transform infoCard; //ref to the card attached to this object
    public Sprite selectedSprite; //image when this blip is selected
    public Sprite passiveSprite; //image when this blip is not selected
    public SpriteRenderer infoCardSprite; //ref to info card's sprite

    private SpriteRenderer mySprite; //ref to this objects sprite

    //image control
    private float alphaLevel = 0.0f; //current alpha level of the card

    [Header("Card")]
    //public Transform destination; //transform that the card should move to
    //public float moveSpeed = 5.0f; //speed at which card moves
    //public float growSpeed = 5.0f; //speed at which card scales up
    public float fadeSpeed = 4.0f; //speed at which card fades away
    //public float startScale = 0.1f; //starting scale of image
    //public float desiredScale = 1.0f; //scale intended to grow to

    //private Vector2 startPos; //transform that the card starts at
    //private Vector2 startScale; //starting scale of card

    //control bools
    private bool isSelected = false; //checks to see if current point is selected
    //private bool isShowing = false; //checks to see if current point is showing it's card
    //private bool isScaling = false; //check to see if current point is scaling up
    private bool isFadingOut = false; //check to see if image is currently fading out
    private bool isFadingIn = false; //check to see if image is currently fading in

    [Header("Scene loading")]
    public int nextSceneNumber = 2;

    // Use this for initialization
    void Start () {
        mySprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isSelected)
        {
            FadeInImage();
        }

        else
        {
            FadeOutImage();
        }

        if(isSelected && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene(nextSceneNumber);
        }

        //if (!isSelected && !isFadingOut)
        //{
        //    ResetCard();
        //}
	}

    ////moves the card to set location
    //void MoveCard()
    //{
    //    infoCard.transform.position = Vector2.Lerp(infoCard.transform.position, destination.position, moveSpeed * Time.deltaTime);
    //}

    ////scale the card as it moves
    //void ScaleCard()
    //{
    //    //get local scale values
    //    float scaleX = infoCard.transform.localScale.x;
    //    float scaleY = infoCard.transform.localScale.y;
    //    //scale up
    //    scaleX += growSpeed * Time.deltaTime;
    //    scaleY += growSpeed * Time.deltaTime;
    //    //check if values have gone over intended limit
    //    if (scaleX >= desiredScale)
    //    {
    //        scaleX = desiredScale;
    //        scaleY = desiredScale;
    //    }
    //    //change scale
    //    infoCard.localScale = new Vector2(scaleX, scaleY);
    //}

    ////move card back to start pos
    //void ResetCard()
    //{
    //    //scale back to original size
    //    float scaleX = startScale;
    //    float scaleY = startScale;

    //    infoCard.localScale = new Vector2(scaleX, scaleY);

    //    //move back to original pos

    //    infoCard.transform.position = startPos;

    //    //make card visible again
    //    alphaLevel = 1;
    //    infoCardSprite.color = new Color(1, 1, 1, alphaLevel);
    //}

    //fade image out before moving back to start pos
    void FadeInImage()
    {
        //check to see if image exists
        if (infoCardSprite)
        {
            //change alpha level relative to time
            alphaLevel += fadeSpeed * Time.deltaTime;
            //change image alpha based on alpha level
            infoCardSprite.color = new Color(1, 1, 1, alphaLevel);
            //keep values between 0 and 1
            alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
            if (alphaLevel <= 0)
            {
                isFadingIn = false;
            }
        }
    }

    //fade image out before moving back to start pos
    void FadeOutImage()
    {
        //check to see if image exists
        if (infoCardSprite)
        {
            //change alpha level relative to time
            alphaLevel -= fadeSpeed * Time.deltaTime;
            //change image alpha based on alpha level
            infoCardSprite.color = new Color(1, 1, 1, alphaLevel);
            //keep values between 0 and 1
            alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
            if (alphaLevel <= 0)
            {
                isFadingOut = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if cursor
        if (collision.CompareTag("Cursor"))
        {
            //change sprite
            mySprite.sprite = selectedSprite;
            //change show card, is selected
            isSelected = true;
            isFadingIn = true;

            //if get input from controller, move scenes
            if (Input.GetKeyDown(KeyCode.Joystick1Button0)){
                SceneManager.LoadScene(nextSceneNumber);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //check if cursor left
        if (collision.CompareTag("Cursor"))
        {
            //change sprite
            mySprite.sprite = passiveSprite;
            //change is selected, is fading
            isSelected = false;
            isFadingOut = true;
        }
    }
}
