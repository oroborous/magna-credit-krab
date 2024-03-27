using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCard : Item
{
    public GameObject lightningSprite;
   
    public Material m_Grey;
    Renderer r;
    private bool bum = false;
    public Claw[] claws;

    private void Start()
    {
        r = GetComponent<Renderer>();
        claws = GameObject.FindObjectsOfType<Claw>();
    }

    private void Update()
    {
        if (!bum)
        {
            foreach (Claw thisClaw in claws)
            {
                if (thisClaw.grabbedItem != null)
                {
                    if (thisClaw.grabbedItem == this)
                    {
                        cardVoid();
                    }
                }

            }
        }
        

    }

    public void cardVoid()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.spawnSprite(3, transform);
        r.material = m_Grey;
        bum = true;
    }

}
