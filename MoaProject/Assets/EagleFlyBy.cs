using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFlyBy : MonoBehaviour {

    public bool DoMove = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(DoMove)
        {
            transform.position += (transform.up * 60) * Time.deltaTime; 
        }
	}

    public void ReleaseTheEagle()
    {
        GetComponent<MeshRenderer>().enabled = true;
        DoMove = true;
    }
}
