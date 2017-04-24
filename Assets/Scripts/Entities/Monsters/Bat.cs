using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster {
    public bool agrro = false;

    public int height;

    void Update()
    {
        FocusOn();
        if (Mathf.Abs(focused.transform.position.x - transform.position.x) < 5)
        {
            agrro = true;
        }
        else
        {
            agrro = false;
        }
        if (agrro == true)
        {
            MoveTo(focused.transform.position.x, focused.transform.position.y);
            Debug.Log("j'ai aggro");
        }
        else
        {
            Patrol();
        }
    }

    public override void Patrol()
    {
        countdown -= 3;
        if (countdown > 0)
        {
            if (transform.position.y >= height)
            {
                MoveTo(-10000000000);
            }
            else
            {
                MoveTo(-100000000, -height);
            }
        }
        if (countdown < 0)
        {
            if (transform.position.y >= height)
            {
                MoveTo(10000000000);
            }
            else
            {
                MoveTo(100000000, -height);
            }
        }
        if (countdown <= -60)
        {
            countdown = 60;
        }
    }
}
