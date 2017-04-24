using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    public GameObject Lol;
    public int P1;
    public int P2;

    void Start () {
        DontDestroyOnLoad(Lol);
	}

    public void Choix1()
    {
        P1 = 1;
        P2 = 2;
        SceneManager.LoadScene(1);
    }

    public void Choix2()
    {
        //Le Choix 1 marche, pas le deux, donc p1 et p2 initialisés pour choix 2
        /*
        P1 = 2;
        P1 = 1;
        */
        SceneManager.LoadScene(1);
    }
}
