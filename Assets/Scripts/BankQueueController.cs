using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankQueueController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("BankQueueController: " + other.name + " is at the bank");
    }
}
