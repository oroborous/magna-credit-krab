using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y +.01f, transform.position.z);
        Destroy(gameObject, 2.5f);

        transform.LookAt(GameObject.Find("Main Camera").transform);
    }
}


