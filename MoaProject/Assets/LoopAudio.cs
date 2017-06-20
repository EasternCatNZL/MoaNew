using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAudio : MonoBehaviour {

    public bool Loop = false;
    public float Delay = 10.0f;
    private float lastTime;
    private AudioSource Audio;

	// Use this for initialization
	void Start () {
        lastTime = 0.0f;
        Audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Loop)
        {
            if (Time.time - lastTime > Delay)
            {
                if (Audio.clip)
                {
                    Audio.Play();
                }
                lastTime = Time.time;
            }
        }
	}
}
