using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piques : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Entity>().IsHit(1);
            other.gameObject.GetComponent<Entity>().GetBumped(transform.position.x);
            Debug.Log("other (bumped) : " + other.gameObject.name + "\nThis : " + name);
        }
    }
}
