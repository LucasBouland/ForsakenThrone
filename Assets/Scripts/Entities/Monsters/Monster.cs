using UnityEngine;
using System.Collections;

public class Monster : Entity {


    public GameObject player1;
    public GameObject player2;

    public GameObject focused;

    public int countdown = 60;

    public virtual void Attack(bool Att)
    {
        if (Att == true)
        {
            gameObject.GetComponent<Monster>().Speed = 3;
        }
        if (Att == false)
        {
            gameObject.GetComponent<Monster>().Speed = 1;
        }
    }

    public virtual void Attack()
    {
        // animation d'attaque
    }

    public virtual void Patrol()
    {
        countdown -= 3;
        if (countdown > 0)
        {
            MoveTo(-100000000);
        }
        if (countdown < 0)
        {
            MoveTo(100000000);
        }
        if (countdown <= -60)
        {
            countdown = 60;
        }
    }
    public virtual void MoveTo(float posX)
    {
        // Fonction de déplacement selon 1 axe
        posX = transform.position.x - posX;
        transform.position += new Vector3((posX / Mathf.Abs(posX)) * Time.deltaTime * Speed, 0, 0);

    }

    public virtual void MoveTo(float posX, float posY)
    {
        // Déplacement selon 2 axes
        posX = transform.position.x - posX;
        posY = transform.position.x - posY;
        transform.position -= new Vector3((posX / Mathf.Abs(posX)) * Time.deltaTime * Speed,
            (posY / Mathf.Abs(posY)) * Time.deltaTime * Speed,
            0);
    }

    public virtual void MoveTo(GameObject objet)
    {
        // Déplacement selon un gameobject
        float x = transform.position.x - objet.transform.position.x;
        transform.position -= new Vector3((x / Mathf.Abs(x)) * Time.deltaTime * Speed, 0, 0);
    }

    public virtual void FocusOn()
    {
        if (Mathf.Abs(transform.position.x - player1.transform.position.x) > Mathf.Abs(transform.position.x - player2.transform.position.x))
        {
            focused = player2;
        }
        else
        {
            focused = player1;
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Entity>().IsHit(Damage);
            other.gameObject.GetComponent<Entity>().GetBumped(transform.position.x);
            Debug.Log("other (bumped) : " + other.gameObject.name + "\nThis : " + name);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack(false);
        }
    }


}
