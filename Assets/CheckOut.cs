using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    public GameObject cashSprite;
    public GameObject costSprite;
    public GameObject bumSprite;

    private Claw leftClaw, rightClaw;

    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        leftClaw = GameObject.Find("L_Claw").gameObject.GetComponent<Claw>();
        rightClaw = GameObject.Find("R_Claw").gameObject.GetComponent<Claw>();
    }

    private void CheckOutItem(Transform purchaser, Item item)
    {
        if (item != null)
        {
            if (item.gameObject.tag == "Money")
            {
                gm.Spend(item.amount);
                gm.spawnSprite(0, purchaser);

                gm.PlaySFX(gm.paid, .4f);

            }
            if (item.gameObject.tag == "Item")
            {
                gm.deposited += item.cost;
                gm.spawnSprite(1, purchaser);
                gm.PlaySFX(gm.cost, .4f);
            }
            if (item.gameObject.tag == "Credit")
            {
                gm.spawnSprite(2, purchaser);
                gm.PlaySFX(gm.bum, .8f);
            }
            Destroy(item.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Shopper")
        {
            ShopperController shopper = other.gameObject.GetComponent<ShopperController>();
            if (shopper.HasItem() & shopper.HasMoney())
            {
                shopper.CheckoutComplete();
            }
        }
        else if (other.gameObject.name == "Crab")
        {
            Item redeemedItem = leftClaw.CheckOutItem();
            CheckOutItem(other.transform, redeemedItem);
            redeemedItem = rightClaw.CheckOutItem();
            CheckOutItem(other.transform, redeemedItem);
        }
    }
}

