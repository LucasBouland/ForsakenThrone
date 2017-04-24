using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBGNewGame : MonoBehaviour {

    public Color lerpedColor = Color.white;
    public GameObject Background;
    public bool change = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void Swap()
    {
        Background.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time, 1));
        while (Background.GetComponent<SpriteRenderer>().color != Color.clear)
        {
            Background.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time, 1));
        }

    }
}
