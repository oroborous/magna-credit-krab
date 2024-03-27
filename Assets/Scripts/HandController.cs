using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject item;
    public GameObject cash, creditCard;

    public bool isLeftHand = false;
    public Claw[] claws;

    void Start()
    {
        if (isLeftHand)
        {
            item = Instantiate(creditCard, transform.position, Quaternion.identity);
        }
        claws = GameObject.FindObjectsOfType<Claw>();
    }

    void Update()
    {
        if (item != null)
        {
            foreach (Claw thisClaw in claws)
            {
                if (thisClaw.grabbedItem != null)
                {
                   //Debug.Log("Grabbed Item: " + thisClaw.grabbedItem + " shopper Item: " + item);
                    if (GameObject.ReferenceEquals(thisClaw.grabbedItem.gameObject, item))
                    {
                        //Debug.Log("Yeah This is that object");

                        item = null;
                        return;
                    }
                }

            }
            item.transform.position = Vector3.Lerp(item.transform.position, transform.position, 1f);
        }

        
    }

    public void DestroyItem()
    {
        Destroy(item.gameObject);
    }
    
    public void GetCashFromBank()
    {
        if (item == null)
        {
            item = Instantiate(cash, transform.position, Quaternion.identity);
        }
    }


    public bool IsHandEmpty()
    {
        return item == null;
    }

    public bool HasMoney()
    {
        return !IsHandEmpty() && (item.name.StartsWith("Dolla") || item.name.StartsWith("Credit"));
    }

    public bool HasItem()
    {
        return !IsHandEmpty() && !(item.name.StartsWith("Dolla") || item.name.StartsWith("Credit"));
    }


  
}
