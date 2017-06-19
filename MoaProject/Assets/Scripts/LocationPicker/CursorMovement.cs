using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour {

    [Header("Movement vars")]
    public float moveSpeed = 3.0f; //speed cursor moves at

    //physics ref
    private Rigidbody2D rigid; //ref to rigidbody

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveCursor();
	}

    //Move the cursor
    void MoveCursor()
    {
        float xChange = 0.0f;
        float yChange = 0.0f;
        //check for input
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //print("Getting horizontal input");
            //get horizontal change
            xChange = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            //print("Getting vertical input");
            //get horizontal change
            yChange = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        }
        //move cursor
        transform.Translate(new Vector2(xChange, yChange));
    }
}
