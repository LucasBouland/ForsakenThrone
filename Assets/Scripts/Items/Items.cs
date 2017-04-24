using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {
    
    public void useItem(GameObject Character,string itemName)
    {
        switch (itemName)
        {
            case "ItemHealthPotion":
                if (Character.GetComponent<Characters>().Health == Character.GetComponent<Characters>().HealthMax)
                {
                    return;
                }
                if (Character.GetComponent<Characters>().Health < Character.GetComponent<Characters>().HealthMax)
                {
                    Character.GetComponent<Characters>().Health += 3;
                    if (Character.GetComponent<Characters>().Health >= Character.GetComponent<Characters>().HealthMax)
                    {
                        Character.GetComponent<Characters>().Health = Character.GetComponent<Characters>().HealthMax;
                    }
                    // moche mais bon :(
                    Character.GetComponent<Characters>().UICheckCases(GetComponent<Characters>().UIHealthPoint, GetComponent<Characters>().Health, GetComponent<Characters>().HealthMax);
                    Character.GetComponent<Characters>().Item = "";
                    Character.GetComponent<Characters>().ItemImage.sprite = Character.GetComponent<Characters>().NoItem;
                }

                break;
            default:
                Debug.Log("Pas d'item");
                break;
        }
    }
}
