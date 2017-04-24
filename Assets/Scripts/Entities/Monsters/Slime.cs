using UnityEngine;
using System.Collections;

public class Slime : Monster {

    public bool Aggro;
    public bool CanJump;

    private void Start()
    {
        Aggro = false;
        CanJump = true;
    }

    private void Update()
    {
        FocusOn();
        if (Mathf.Abs(focused.transform.position.x - transform.position.x) < 5)
        {
            Aggro = true;
        }
        else
        {
            Aggro = false;
        }
        if (Aggro == true)
        {
            MoveTo(focused);
            if (focused.transform.position.x < gameObject.transform.position.x)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else if (focused.transform.position.x > gameObject.transform.position.x)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            Patrol();
        }
    }

    public override void Attack()
    {

    }

    public override void Patrol()
    {
        countdown -= 1;
        GetComponent<Entity>().Animator.SetTrigger("slimeMove");
        if (countdown > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            MoveTo(-100000000);
        }
        if (countdown < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            MoveTo(100000000);
        }
        if (countdown <= -60)
        {
            countdown = 60;
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CanJump = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" && CanJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
        }
        if (other.tag == "Player")
        {
            GetComponent<Entity>().Animator.SetTrigger("slimeAttack");
            Attack();
        }
    }
}
