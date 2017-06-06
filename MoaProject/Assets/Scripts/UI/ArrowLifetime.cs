using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLifetime : MonoBehaviour {

    public float destroyDelay = 1.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfterDelay());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyAfterDelay()
    {
        //wait brief delay
        yield return destroyDelay;
        //destroy object
        Destroy(gameObject);
    }
}
