using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Entity : MonoBehaviour {

    public int Health;
    public int HealthMax;
    public int Damage;

    public float Speed;
    public float timeFlashDamage = 0.0f;

    public Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void DeathAnimation()
    {
        HitIndicator();

        // inserer l'animation de mort
        Animator.SetTrigger("");
    }

    public virtual void DeathIndicator()
    {
        // mettre un timer pour donner le temps à l'animation de finir
        // emepcher le joueur de faire des actions
        gameObject.SetActive(false);
    }

    public void GetBumped(float posX)
    {
        if (posX > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(-6, 6), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(6, 6), ForceMode2D.Impulse);
        }
    }

    public void GetDamage(int dmg)
    {
        timeFlashDamage = 0.5f;
        Health -= dmg;
    }

    public void HitAnimation()
    {
        // inserer l'animation de hit
       // animator.SetTrigger("");
    }

    public virtual void HitIndicator()
    {
        // afficher ici le changement pour les pv sur UI
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        timeFlashDamage -= Time.deltaTime;
        if (timeFlashDamage < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public bool IsAlive()
    {
        if (Health <= 0) return false;
        else return true;
    }

    public void IsHit(int damage)
    {
        GetDamage(damage);
        if (IsAlive())
        {
            HitAnimation();
            HitIndicator();
        }
        else
        {
            DeathAnimation();
            DeathIndicator();
        }
    }
}
