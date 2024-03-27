using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : Item
{
    

    void Start()
    {
        amount = Random.Range(1, 5) * 20;
        Debug.Log("Cash: Created with amount " + amount);
    }
}
