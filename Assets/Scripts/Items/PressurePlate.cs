using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public GameObject door;

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("test pressure plate");
        if (other.gameObject.tag == "Player")
        {
            door.SetActive(false);
        }
    }
}
