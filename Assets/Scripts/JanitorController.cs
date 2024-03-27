using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorController : WanderingAI
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("JanitorController: Encountered " + other.gameObject.name);

        if (other.gameObject.tag == "Item" || other.gameObject.tag == "Money" || other.gameObject.tag == "Credit")
        {
            Destroy(other.gameObject);
        }
    }
}
