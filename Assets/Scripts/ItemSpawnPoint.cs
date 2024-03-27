using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    private List<Item> myItems = new List<Item>();

    public int maxItems;
    public Item itemPrefab;
    public float spawnTimer = 1.5f;

    private float timer = 0;
    

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {

            if (maxItems > myItems.Count)
            {
                Item item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                myItems.Add(item);
            }
            timer = 0;
        }
    }
}
