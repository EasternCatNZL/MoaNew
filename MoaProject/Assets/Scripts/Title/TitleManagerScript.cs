using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManagerScript : MonoBehaviour {

    //Image refs
    [Header("Images")]
    public Image startArrow; //reference to start arrow
    public Image creditArrow; //reference to credit arrow
    public Image quitArrow; //reference to end arrow
    public Image Credits; //ref to credits page

    private bool inCredits = false; 

    /*
     * button choice:
     * 0 = start
     * 1 = credit
     * 2 = quit
     */
    int currentButton = 0; //int reference to currently selected button
    public bool canInput = false; //checks to see if input from controller axis allowed

	// Use this for initialization
	void Start () {
        //set can input when starting to true
        canInput = true;
	}
	
	// Update is called once per frame
	void Update () {
        ChangeChoice();
        CheckJoystickFree();
        GetSelection();
	}

    //changes the choice on start menu
    void ChangeChoice()
    {
        //check if player is allowed input
        if (canInput)
        {
            if (inCredits && Input.anyKeyDown)
            {
                Credits.color = new Color(1, 1, 1, 0);
                inCredits = false;
            }
            //checks if the joystick has recieved any input
            if (Input.GetAxis("Horizontal") != 0)
            {
                int increment;
                if (Input.GetAxis("Horizontal") > 0) increment = 1;
                else increment = -1;

                
                if (currentButton == 0)
                {
                    //change seleceted button
                    if (increment > 0)
                    {
                        currentButton = 1;
                        //change graphical output
                        startArrow.color = new Color(1, 1, 1, 0);
                        creditArrow.color = new Color(1, 1, 1, 1);
                        quitArrow.color = new Color(1, 1, 1, 0);
                    }
                    else
                    {
                        currentButton = 2;
                        //change graphical output
                        startArrow.color = new Color(1, 1, 1, 0);
                        creditArrow.color = new Color(1, 1, 1, 0);
                        quitArrow.color = new Color(1, 1, 1, 1);
                    }
                }
                else if (currentButton == 1)
                {
                    //change seleceted button
                    if (increment > 0)
                    {
                        currentButton = 2;
                        //change graphical output
                        startArrow.color = new Color(1, 1, 1, 0);
                        creditArrow.color = new Color(1, 1, 1, 0);
                        quitArrow.color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        currentButton = 0;
                        //change graphical output
                        startArrow.color = new Color(1, 1, 1, 1);
                        creditArrow.color = new Color(1, 1, 1, 0);
                        quitArrow.color = new Color(1, 1, 1, 0);
                    }
                }
                else if (currentButton == 2)
                {
                    //change seleceted button
                    if (increment > 0)
                    {
                        currentButton = 0;
                        //change graphical output
                        startArrow.color = new Color(1, 1, 1, 1);
                        creditArrow.color = new Color(1, 1, 1, 0);
                        quitArrow.color = new Color(1, 1, 1, 0);
                    }
                    else
                    {
                        currentButton = 1;
                        //change graphical output
                        startArrow.color = new Color(1, 1, 1, 0);
                        creditArrow.color = new Color(1, 1, 1, 1);
                        quitArrow.color = new Color(1, 1, 1, 0);
                    }
                }
                canInput = false;
            }
        }
    }

    //checks if the joystick input is free
    void CheckJoystickFree()
    {
        //if no input, then allow input to change state
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            canInput = true;
        }
    }

    void GetSelection()
    {
        //check input for controller submit
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            //check which button is currently selected
            if (currentButton == 0)
            {
                //load into game scene
                SceneManager.LoadScene(1);
            }
            else if(currentButton == 1)
            {
                Credits.color = new Color(1, 1, 1, 1);
                inCredits = true;
            }
            else if (currentButton == 2)
            {
                //quit application
                Application.Quit();
            }
        }
    }
}
