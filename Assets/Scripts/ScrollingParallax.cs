using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingParallax : MonoBehaviour {

    public GameObject Camera;

    public float ParallaxSpeed;
    private float PrevLocationX = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float Movement = Camera.transform.position.x;
        if (PrevLocationX > Movement)
        {
            float x = 1 * Time.deltaTime * ParallaxSpeed;
            transform.Translate(x, 0, 0);
        }
        else if (PrevLocationX < Movement)
        {
            float x = -1 * Time.deltaTime * ParallaxSpeed;
            transform.Translate(x, 0, 0);
        }
        else
        {
            // do nothing
        }
        PrevLocationX = Movement;
	}
}
