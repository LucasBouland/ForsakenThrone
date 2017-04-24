using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {

    public int Damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name.Contains("Monster"))
        {
            other.gameObject.GetComponent<Entity>().IsHit(Damage);
            other.gameObject.GetComponent<Entity>().GetBumped(transform.position.x);
        }
    }
    }
