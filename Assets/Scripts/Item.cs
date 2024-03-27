using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool leftBounds;
    public bool rightBounds;
    public bool beingCarried;
    //bool inCheck = false;

    public int cost;
    public int amount;

    public bool makeSound = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "L_Claw")
        {
            leftBounds = true;
            GameManager gm = FindObjectOfType<GameManager>();
            if (makeSound)
            {
                gm.PlaySFX(gm.hit[Random.Range(0, 4)], .1f);
            }
        }
        if (other.gameObject.tag == "R_Claw")
        {
            rightBounds = true;
            GameManager gm = FindObjectOfType<GameManager>();
            if (makeSound)
            {
                gm.PlaySFX(gm.hit[Random.Range(0, 4)], .1f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "L_Claw")
        {
            leftBounds = false;
        }
        if (other.gameObject.tag == "R_Claw")
        {
            rightBounds = false;
        }

    }


    private void FixedUpdate()
    {
        if (beingCarried)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

}

