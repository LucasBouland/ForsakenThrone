using UnityEngine;
using System.Collections;
using System;

public class Wizard : Characters, ISkills
{

    public GameObject SimpleProjectile;

    public void Jump(GameObject Character)
    {
        PlayerAudioSource.clip = JumpSound;
        PlayerAudioSource.Play();
        Character.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
        Animator.SetTrigger(Character.GetComponent<PlayerController>().CharacterName + "Jump");
        Animator.SetBool("IsJumping", true);
    }

    public void SimpleAttack(GameObject Character, bool direction)
    {
        PlayerAudioSource.clip = AttackSound;
        PlayerAudioSource.Play();
        Vector3 pos = Weapon.transform.localPosition;
        pos.x = 0.5f;
        // voir si y et z necessaires
        pos.y = 0f;
        pos.z = 0f;
        pos.x = direction ? Mathf.Abs(pos.x) : Mathf.Abs(pos.x) * (-1);
        Weapon.transform.localPosition = pos;
        Weapon.SetActive(true);
        if (Animator.GetBool("IsJumping"))
        {
            Animator.SetTrigger("wizardJumpAttack");
        }
        else
        {
            Animator.SetTrigger("wizardAttack");
        }
    }

    //lance boule de feu
    public void Special1(GameObject Character, bool direction)
    {
        // Walk nom temp -> devrait etre fireball
        PlayerAudioSource.clip = WalkSound;
        PlayerAudioSource.Play();
        Ressource--;
        int x = direction ? 1 : -1;
        Vector3 projection = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
        GameObject Attack = Instantiate(SimpleProjectile, projection, transform.rotation);
        Animator.SetTrigger("wizardFireBall");
        Attack.GetComponent<WizardProjectile>().Speed = 10;
        Attack.GetComponent<WizardProjectile>().Direction = x;
        Attack.GetComponent<WizardProjectile>().Damage = 1;
        if (x < 0 )
            Attack.GetComponent<SpriteRenderer>().flipX = true;
        Attack.SetActive(true);
    }

}
