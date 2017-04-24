using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Characters test;
    private GameObject Character;

    public GameObject choice;
    public GameObject OtherCharacter;
    public GameObject Block;

    public Animator AnimatorController;

    public Characters CharScript;
    
    public SpriteRenderer Sprite;

    public Transform SwitchPos;

    public string CharacterName;

    public int playerNumber;

    public GameObject Canvas;

    public bool CanJump = true;
    public bool CanMove = true;
    public bool SpriteDirection = true;
    public bool CanSwitch = false;
    public bool SwitchOk = false;
    public bool IsAttacking;
    public bool IsRegenOn;
    public float PlayerSpeed;

    private void Start()
    {
        choice = GameObject.Find("CharacterSetup");
        IsRegenOn = false;
        Character = gameObject;
        CharScript = Character.GetComponent<Characters>();
        Sprite = Character.GetComponent<SpriteRenderer>();
        AnimatorController = Character.GetComponent<Animator>();
        CharacterName = Character.name.ToLower();
        PlayerSpeed = Character.GetComponent<Characters>().Speed;
        if (CharacterName == "warrior")
        {
            CharScript.Skills = gameObject.GetComponent<Warrior>();
            playerNumber = choice.GetComponent<DontDestroy>().P1;
            Debug.Log("t" + CharScript.Skills);
        }
        else
        {
            CharScript.Skills = gameObject.GetComponent<Wizard>();
            playerNumber = choice.GetComponent<DontDestroy>().P2;
        }
    }

    void Update()
    {
        if (AnimatorController.GetBool("IsJumping"))
        {
            CanJump = false;
        }
        CharacterMovement();
        CharacterAction();
        CharacterTimer();
    }

    private IEnumerator RegenRessource()
    {
        yield return new WaitForSeconds(1);
        IsRegenOn = false;
        CharScript.Ressource += 1;
    }

    void CharacterAction()
    {
        // Saut
        if (Input.GetButtonDown("A_P" + playerNumber) && CanJump)
        {
            CharScript.UseJump(Character);
        }
        // Attaque simple
        if (Input.GetButtonDown("B_P" + playerNumber) && !IsAttacking)
        {
            CharScript.UseAttack(Character, SpriteDirection);
        }
        // Utiliser item
        if (Input.GetButtonDown("X_P" + playerNumber))
        {
            Character.GetComponent<Items>().useItem(Character, CharScript.Item);
        }
        // block ou rien
        if (Input.GetButtonDown("Y_P" + playerNumber))
        {
            CharScript.Skill3(Character, SpriteDirection);
        }
        if (Input.GetButtonUp("Y_P" + playerNumber))
        {
            CharScript.Skill3End();
        }
        // dash ou boule de feu
        if (Input.GetButtonDown("R2_P" + playerNumber) && (CharScript.Ressource >= 4))
        {
            CharScript.UseSpecial1(Character, SpriteDirection);
        }
        // bouton echap
        if (Input.GetButtonDown("Pause"))
        {
            Canvas.SetActive(true);
            Time.timeScale = 0;
        }
        // tp un perso vers un autre
        if (Input.GetButton("L2_P" + playerNumber) && CharScript.Ressource >= 3)
        {
            CanSwitch = true;
            SwitchPos = gameObject.transform;
            if (OtherCharacter.GetComponent<PlayerController>().CanSwitch == true)
            {
                SwitchCharacter();
            }
        }
        if (Input.GetButtonUp("L2_P" + playerNumber))
        {
            CanSwitch = false;
        }
    }

    void CharacterMovement()
    {
        if ((Input.GetAxis("Horizontal_P" + playerNumber) * Time.deltaTime * PlayerSpeed ) != 0)
        {
            float x = Input.GetAxis("Horizontal_P" + playerNumber) * Time.deltaTime * PlayerSpeed;
            if (x < 0)
            {
                Sprite.flipX = false;
                SpriteDirection = false;
                if (!AnimatorController.GetBool("IsJumping"))
                {
                    CharScript.Animator.SetTrigger(CharacterName + "Walk");
                }             
                float t = -1 * Time.deltaTime * PlayerSpeed;
                transform.Translate(t, 0, 0);

            }
            if (x > 0)
            {
                Sprite.flipX = true;
                SpriteDirection = true;
                if (!AnimatorController.GetBool("IsJumping"))
                {
                    CharScript.Animator.SetTrigger(CharacterName + "Walk");
                }
                float t = 1 * Time.deltaTime * PlayerSpeed;
                transform.Translate(t, 0, 0);
            }
        }
        else
        {
            GetComponent<Characters>().Animator.SetTrigger(CharacterName + "Idle");
        }
        // TODO block vertical
        // TODO drop platform
    }
    void CharacterTimer()
    {

        if (CharScript.Ressource >= CharScript.RessourceMax)
            return;
        if (IsRegenOn == false)
        {
            IsRegenOn = true;
            StartCoroutine(RegenRessource());
        }
    }

    // tp un personnage vers un autre
    void SwitchCharacter()
    {
        CharScript.Ressource -= 3;
        PlayerSpeed = 0;
        Transform pos = OtherCharacter.GetComponent<PlayerController>().SwitchPos;
        Vector3 newPos = new Vector3(pos.position.x, pos.position.y, pos.position.z);
        gameObject.transform.position = newPos;
        PlayerSpeed = 4;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.Equals("Item") & CharScript.Item == "")
        {
            CharScript.Item = other.gameObject.name;
            CharScript.ItemImage.sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
            Destroy(other.gameObject);
            if (CharScript.Item.EndsWith(")"))
            {
                CharScript.Item = CharScript.Item.Remove(CharScript.Item.IndexOf(' '));
            }
        }
        if (other.transform.tag.Equals("Ground"))
        {
            CanJump = true;
            AnimatorController.SetBool("IsJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag.Equals("Ground"))
        {
            CanJump = false;
        }
    }
}
