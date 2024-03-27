using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperSpawnPoint : MonoBehaviour
{

    public ShopperController[] shoppers;
    public float interval = 5.0f;
    private float timer;
    
    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            int randomIndex = Random.Range(0, shoppers.Length);
            Instantiate(shoppers[randomIndex], transform.position, transform.rotation);
            timer = 0;
        }
    }
}