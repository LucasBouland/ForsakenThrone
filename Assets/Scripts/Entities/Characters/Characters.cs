using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Characters : Entity {

    public ISkills Skills;

    public AudioSource PlayerAudioSource;

    public AudioClip JumpSound;
    public AudioClip AttackSound;
    public AudioClip WalkSound;
    
    public Transform HealthCases;
    public Transform ClassCases;
    public Transform LifeCases;

    public List<Transform> UIHealthPoint = new List<Transform>();
    public List<Transform> UIClassPoint = new List<Transform>();
    public List<Transform> UILifeRemaining = new List<Transform>();

    public GameObject gameOver;
    public GameObject gameOverImage;
    public GameObject Weapon;
    public GameObject Score;

    public Image ItemImage;
    public Sprite NoItem;

    public string Item;
    // trouver un nom plus descriptif que Life
    public int Life;
    public int LifeMax;
    public int Ressource;
    public int RessourceMax;

    private void Start()
    {
        PlayerAudioSource = gameObject.GetComponent<AudioSource>();
        // l'ajout ou retrait de case peut etre rendu plus modulaire
        FindChildCases(UIHealthPoint, HealthCases);
        FindChildCases(UIClassPoint, ClassCases);
        FindChildCases(UILifeRemaining, LifeCases);
        Life = 3;
        LifeMax = 3;
        Health = 5;
        HealthMax = 5;
        Ressource = 5;
        RessourceMax = 5;
    }

    private void Update()
    {
        UICheckCases(UIClassPoint, Ressource, RessourceMax);
        HitIndicator();
    }

    public IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(0);
    }
    public IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(3);
        gameOverImage.SetActive(true);
        
    }
    public void UICheckCases(List<Transform> UICases, int current, int max)
    {
        for (int i = 1; i <= max; i++)
        {
            if (i <= current)
            {
                UICases[i].gameObject.SetActive(true);
            }
            else
            {
                UICases[i].gameObject.SetActive(false);
            }
        }
    }

    // ajoute tous les enfants d'un GameObject dans la liste
    public void FindChildCases(List<Transform> UIList, Transform Container)
    {
        Transform[] allChildren = Container.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            UIList.Add(child);
        }
    }

    public override void HitIndicator()
    {
        UICheckCases(UIHealthPoint, Health, HealthMax);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        timeFlashDamage -= Time.deltaTime;
        if (timeFlashDamage < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public override void DeathIndicator()
    {
        // l'animation plante la suite 
        //animator.SetTrigger(GetComponent<PlayerController>().CharacterName + "Death");
        gameObject.SetActive(false);
        CheckLivesRemaining();
    }

    public void CheckLivesRemaining()
    {
        Life--;
        if (Life <= 0)
        {
            if (gameObject.GetComponent<PlayerController>().OtherCharacter.GetComponent<Characters>().Life <= 0 && (gameObject.GetComponent<PlayerController>().OtherCharacter.activeSelf == false))
            {
                gameObject.SetActive(true);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                gameOver.SetActive(true);
                StartCoroutine(GameOverScreen());
                StartCoroutine(BackToMainMenu());
            }
            else
            {
                UICheckCases(UILifeRemaining, Life, LifeMax);
                gameObject.GetComponent<PlayerController>().OtherCharacter.GetComponent<Characters>().Life--;
            }
        }  
        else
        {
            // respawn
            UICheckCases(UILifeRemaining, Life, LifeMax);
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            gameObject.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y,0);
            Health = HealthMax;
            UICheckCases(UIHealthPoint, Health, HealthMax);
            gameObject.SetActive(true);
        }
    }

    // A 
    public void UseJump(GameObject Character)
    {
        Skills.Jump(Character);
    }

    // B
    public void UseAttack(GameObject Character, bool direction)
    {
        Skills.SimpleAttack(Character, direction);
    }

    // Y
    public virtual void Skill3(GameObject Character, bool direction)
    {
        // Competence 1
    }

    public virtual void Skill3End()
    {
        // Competence 1
    }

    // R2
    public void UseSpecial1(GameObject Character, bool direction)
    {
        Skills.Special1(Character, direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {    
        if (other.transform.tag.Equals("Coin"))
        {
            int a = Int32.Parse(Score.transform.GetComponent<Text>().text);
            a++;
            string score = a.ToString();
            Score.transform.GetComponent<Text>().text = score;
            other.gameObject.SetActive(false);
        }
    }

}
