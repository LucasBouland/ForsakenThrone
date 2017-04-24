using UnityEngine;
using System.Collections;
using System;

public class Warrior : Characters, ISkills
{

    public GameObject Block;
    public GameObject BlockOnSides;
    public GameObject DashEffects;

    public void Jump(GameObject Character)
    {
        PlayerAudioSource.clip = JumpSound;
        PlayerAudioSource.Play();
        Character.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
        Animator.SetTrigger(Character.GetComponent<PlayerController>().CharacterName + "Jump");
        Animator.SetBool("IsJumping", true);
    }

    // coup d'epée
    public void SimpleAttack(GameObject Character, bool direction)
    {
        PlayerAudioSource.clip = AttackSound;
        PlayerAudioSource.Play();
        Vector3 pos = Weapon.transform.localPosition;
        pos.x = 0.15f;
        // voir si y et z necessaires
        pos.y = 0f; 
        pos.z = 0f;
        pos.x = direction ? Mathf.Abs(pos.x) : Mathf.Abs(pos.x) * (-1);
        Weapon.transform.localPosition = pos;
        Weapon.SetActive(true);
        if (Animator.GetBool("IsJumping"))
        {
            Animator.SetTrigger("warriorJumpAttack");
        }
        else
        {
            Animator.SetTrigger("warriorAttack");
        }
    }
    // block top + direction
    public override void Skill3(GameObject Character, bool direction)
    {
        if ((Input.GetAxis("Horizontal_P" + Character.GetComponent<PlayerController>().playerNumber)) == 0)
        {
            // block vers le haut
            Animator.SetBool("IsBlocking", true);
            Animator.SetTrigger("warriorBlockTop");
            Block.SetActive(true);
        }
        else
        {
            // block vers le téco
            if (direction)
            {
                BlockOnSides.transform.localPosition = new Vector3(Mathf.Abs(BlockOnSides.transform.localPosition.x), 0, 0);
            }
            else
            {
                BlockOnSides.transform.localPosition = new Vector3(Mathf.Abs(BlockOnSides.transform.localPosition.x) * -1, 0, 0);
            }
            Animator.SetBool("IsBlocking", true);
            Animator.SetTrigger("warriorBlock");
            BlockOnSides.SetActive(true);
        }
    }

    public override void Skill3End()
    {
        Animator.SetBool("IsBlocking", false);
        Block.SetActive(false);
        BlockOnSides.SetActive(false);
    }

    //dash
    public void Special1(GameObject Character, bool direction)
    {
        Vector3 dashPos = DashEffects.transform.localPosition;
        Ressource -= 4;
        float dash = direction ? 15 : -15;
        GetComponent<Characters>().Animator.SetTrigger("warriorDash");
        if (dash > 1)
        {
            dashPos.x = 0.9f;
            DashEffects.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            dashPos.x = -0.9f;
            DashEffects.GetComponent<SpriteRenderer>().flipX = false;
        }
        DashEffects.transform.localPosition = dashPos;
        gameObject.GetComponent<PlayerController>().PlayerSpeed = Speed;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dash, 5F), ForceMode2D.Impulse);
        // 5 pour coller avec la barre de stamina, à regler en fonction de ce qui est necessaire
    }
}
