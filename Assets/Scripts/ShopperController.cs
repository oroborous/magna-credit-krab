using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperController : WanderingAI
{
    public float itemReach = 5f;

    private Vector3 bankQueueLocation;
    private Vector3 checkoutLaneLocation;
    private Vector3 exitLocation;
    private HandController leftHand;
    private HandController rightHand;
    private Item targetItem;
    private bool hasPurchased = false;

    void Start()
    {
        Setup();
        StartCoroutine("checkemotion");
        bankQueueLocation = GameObject.Find("BankQueue").transform.position;
        checkoutLaneLocation = GameObject.Find("CheckoutLane").transform.position;
        exitLocation = GameObject.Find("Exit").transform.position;
        leftHand = transform.Find("L_Hand").gameObject.GetComponent<HandController>();
        rightHand = transform.Find("R_Hand").gameObject.GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPurchased)
        {
            ExitAndDie();
        }
        else if (!HasMoney())
        {
            WalkTowardBank();
        }
        else if (!HasItem())
        {
            // Are you heading toward an item?
            if (targetItem != null)
            {
                // Has the item been picked up yet?
                if (targetItem.beingCarried)
                {
                    targetItem = null;
                }
                else
                {
                    // Is the item within reach yet?
                    if (Vector3.Distance(transform.position, targetItem.transform.position) < itemReach)
                    {
                        rightHand.item = targetItem.gameObject;
                        targetItem.beingCarried = true;
                        targetItem.transform.parent = null;
                    }
                    else
                    {
                        agent.SetDestination(targetItem.transform.position);
                    }
                }
            }
            else if (Random.Range(1, 100) < 3) {
                // Maybe browse a bit
                FindItemToBuy();
            }
        }
        else
        {
            WalkTowardCheckout();
        }
    }

    public void CheckoutComplete()
    {
        hasPurchased = true;
        leftHand.DestroyItem();
        rightHand.DestroyItem();
        GameManager gm = FindObjectOfType<GameManager>();
        gm.PlaySFX(gm.happy, .05f);
    }

    void FindItemToBuy()
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Item");
        int randomIndex = Random.Range(0, allItems.Length);
        
        Item item = allItems[randomIndex].GetComponent<Item>();
            
        if (!item.beingCarried)
        {
            targetItem = item;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name == "BankQueue")
        {
            //Debug.Log("ShopperController: At bank queue");
            if (!HasMoney())
            {
                GetMoney();
            }
        }
        else if (other.transform.gameObject.name == "Exit")
        {
            //Debug.Log("ShopperController: Goodbye cruel world");
            Destroy(gameObject);
        }
    }

    public bool HasMoney()
    {
        return leftHand.HasMoney() || rightHand.HasMoney();
    }

    public bool HasItem()
    {
        return leftHand.HasItem() || rightHand.HasItem();
    }

    private void GetMoney()
    {
        if (leftHand.IsHandEmpty())
        {
            leftHand.GetCashFromBank();
        }
    }

    private void WalkTowardBank()
    {
        //Debug.Log("ShopperController: Walking toward bank");

        agent.SetDestination(bankQueueLocation);
        agent.speed = 10;
    }

    private void WalkTowardCheckout()
    {
        //Debug.Log("ShopperController: Walking away from bank");

        agent.SetDestination(checkoutLaneLocation);
        agent.speed = 8;
    }

    private void ExitAndDie()
    {
        //Debug.Log("ShopperController: Walking toward exit");

        agent.SetDestination(exitLocation);
        agent.speed = 10;
    }

    public void IsEmotion()
    {
        //Debug.Log("is emotional");
        if (!HasMoney() && !hasPurchased)
        {
           //Debug.Log("No money");
            GameManager gm = FindObjectOfType<GameManager>();
            Transform n = transform;
            n.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            gm.spawnSprite(4, n);

            if(Random.Range(0,2) == 0)
            {
                gm.PlaySFX(gm.grumble, .15f);
            }
        }
        if (hasPurchased)
        {
            //Debug.Log("Happy");
            GameManager gm = FindObjectOfType<GameManager>();
            Transform n = transform;
            n.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            gm.spawnSprite(5, n);
            
        }

    }

    IEnumerator checkemotion()
    {
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("This is Waiting");
        IsEmotion();
        StartCoroutine("checkemotion");
    }

}
