using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{

    public float dist;
    private Vector3 clawTarget;
    public GameObject ClawHome;
    public GameObject clawhomeL;
    public GameObject clawhomeR;
    public GameObject wrist;
    public GameObject armHome;

    public bool carrying_L;
    public bool carrying_R;
    public Item grabbedItem;
    private LineRenderer armRend;


    private void Start()
    {
        armRend = armHome.GetComponent<LineRenderer>();
    }

    public Item CheckOutItem()
    {
        Item redeemedItem = grabbedItem;
        DropItem();
        return redeemedItem;
     }

    private void Update()
    {
        renderArm();
        heldItem();
    }

    void FixedUpdate()
    {
        ItemDistance();
    }

    public bool HasItem()
    {
        return grabbedItem != null;
    }

    public void ItemDistance()
    {
        //start at infinity
        float distToClosest = Mathf.Infinity;
        Item closest = null;
        Item[] allItems = GameObject.FindObjectsOfType<Item>();

        //For Each Item in the scene, if the distance to the item is less then focus on that item
        foreach (Item currentItem in allItems)
        {
            //If the item you are currently testing distance for is not being held
            if (!currentItem.beingCarried) { 
                float distToItem = (currentItem.transform.position - ClawHome.transform.position).sqrMagnitude;
                if (distToItem < distToClosest)
                {
                    //if dist to current item is a shorter distance than previously, replace closest item with current
                    distToClosest = distToItem;
                    closest = currentItem;
                }
             }
        }

        if (closest == null)
        {
            return;
        }

        //See what it do 
        Debug.DrawLine(this.transform.position, closest.transform.position);

       //if closest item is within grab range, move claw to it
        if (Vector3.Distance(ClawHome.transform.position, closest.transform.position) < dist)
        {
            clawTarget = closest.transform.position;
            ClawMoveToItem(clawTarget);
        }
        else //Move claw to home position
        {
            clawTarget = ClawHome.transform.position;
            ClawMoveToItem(clawTarget);
        }


        //Mouse Input, Grabbing Item. If Left click, left hand is not carrying anything, and this is being called from the left claw
        if (Input.GetMouseButtonUp(0) && !carrying_L && this.gameObject.name == "L_Claw")
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.PlaySFX(gm.grab, .5f);
            //if the item is within the bounds (sphere trigger) of the left hand,
            if (closest.leftBounds == true && !closest.beingCarried)
            {
                grabbedItem = closest;
                carrying_L = true;
                grabbedItem.beingCarried = true;
                grabbedItem.makeSound = false;

                // Remove parent spawn point so it can respawn another item
                grabbedItem.transform.parent = null;
                //Debug.Log("Left Grabbed");
               
            }
        }

        //Mouse input, If Right Click, Right Claw is not carrying anything, and this is being called from Right claw
        if (Input.GetMouseButtonUp(1) && !carrying_R && this.gameObject.name == "R_Claw")
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.PlaySFX(gm.grab, .5f);
            if (closest.rightBounds == true && !closest.beingCarried)
            {
                
                grabbedItem = closest;
                carrying_R = true;
                grabbedItem.beingCarried = true;
                grabbedItem.makeSound = false;

                // Remove parent spawn point so it can respawn another item
                if (grabbedItem.transform != null)
                {
                    grabbedItem.transform.parent = null;
                }
                else
                {
                    Debug.Log("Why is this null? " + grabbedItem);
                }
                //Debug.Log("Right Grabbed");
            }
        }

    }

    public void ClawMoveToItem(Vector3 cTarget)
    {
        //if Left is not carrying anything, and this ie being called from left claw
        if(!carrying_L && this.gameObject.name == "L_Claw")
        {
            //move claw to Target Position
            transform.position = Vector3.Lerp(transform.position, cTarget, .4f);
         
        }
        else if(carrying_L) //if claw is carrying something, move claw to home position again
        {
            transform.position = Vector3.Lerp(transform.position, ClawHome.transform.position, .18f);
        }
        else
        {
            carrying_L = false;
        }


        //if right is not carrying, and this is being called from the right claw
        if (!carrying_R && this.gameObject.name == "R_Claw"){
            transform.position = Vector3.Lerp(transform.position, cTarget, .2f);
          
        }
        else if(carrying_R) //if claw is carrying something, move claw to home position again
        {
            transform.position = Vector3.Lerp(transform.position, ClawHome.transform.position, .18f);
        }
        else
        {
            carrying_R = false;
        }
        

    }


    public void renderArm()
    {
        armRend.SetPosition(0, armHome.transform.position);
        armRend.SetPosition(1, wrist.transform.position);

    }

    public void DropItem()
    {
        if (grabbedItem != null)
        {
            grabbedItem.beingCarried = false;
            grabbedItem = null;
        }
        carrying_L = false;
        carrying_R = false;
    }

    public void heldItem()
    {
        if (carrying_L)
        {
            grabbedItem.transform.position = Vector3.Lerp(transform.position, clawhomeL.transform.position, .05f);
            grabbedItem.transform.rotation = Quaternion.Euler(0, 0, 85);
            grabbedItem.beingCarried = true;
        }
        if (carrying_R)
        {
            grabbedItem.transform.position = Vector3.Lerp(transform.position, clawhomeR.transform.position, .05f);
            grabbedItem.transform.rotation = Quaternion.Euler(0, 0, 85);
            grabbedItem.beingCarried = true;
        }

        //if Middle Mouse is clicked, let go of everything
        if (Input.GetMouseButton(2))
        {
            DropItem();
        }


    }

}
