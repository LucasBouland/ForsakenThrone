using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour {

    public float Speed;

    public int Direction;
    public int Damage;

    private void Update()
    {
        ProjectileTravel();
    }

    private void ProjectileTravel()
    {
        float t = Direction * Time.deltaTime * Speed;
        transform.Translate(t, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name.Contains("Monster") && other.GetComponent<BoxCollider2D>().IsTouching(GetComponent<BoxCollider2D>()))
        {
            other.gameObject.GetComponent<Entity>().IsHit(Damage);
            other.gameObject.GetComponent<Entity>().GetBumped(transform.position.x);
            Destroy(this.gameObject);
        }
        else if ((other.gameObject.tag.Contains("Ground") || other.gameObject.tag.Contains("Wall")) && other.GetComponent<BoxCollider2D>().IsTouching(GetComponent<BoxCollider2D>()))
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
