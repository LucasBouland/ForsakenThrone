using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {

    public Transform Player1;
    public Transform Player2;
    public Camera zoomCamera;
    public int minSize;
    public int maxSize;
    public int size;
    public bool zoom;

    void Start()
    {
        zoomCamera = GetComponentInParent<Camera>();
        minSize = 5;
        maxSize = 7;
        size = 0;
        zoom = true;
    }

    void Update()
    {
        CenterCamera();
        Zoom();
        TestDistance(Player1);
        TestDistance(Player2);
    }
    // centre la camera entre les deux joueurs
    void CenterCamera()
    {
        float x;
        float y;
        if (Player1 == null)
        {
            x = (Player2.position.x);
            y = (Player2.position.y);
        }
        else if (Player2 == null)
        {
            x = (Player1.position.x);
            y = (Player1.position.y);
        }
        else
        {
            x = (Player1.position.x + Player2.position.x) / 2.0f;
            y = (Player1.position.y + Player2.position.y) / 2.0f;
        }

        transform.position = new Vector3(x, y, -10);
    }

    public void TestDistance(Transform player)
    {
        if (transform.position.x - player.position.x > zoomCamera.orthographicSize * 2.1)
        {
            player.parent.gameObject.transform.Translate(new Vector3(0.4f, 0));
        }
        else if (transform.position.x - player.position.x < -1 * (zoomCamera.orthographicSize * 2.1))
        {
            player.parent.gameObject.transform.Translate(new Vector3(-0.4f, 0));
        }
    }

    void Zoom()
    {
        if (zoomCamera.orthographicSize < maxSize && !zoom)
        {
            zoomCamera.orthographicSize += Time.deltaTime;
        }
        else if (zoomCamera.orthographicSize > minSize && zoom)
        {
            zoomCamera.orthographicSize -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            zoom = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            zoom = false;
        }
    }
}
