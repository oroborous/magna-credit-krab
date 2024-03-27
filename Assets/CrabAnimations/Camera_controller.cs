using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 offset;


    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()  // runs at the very end of Update
    {
        // transform auto targets the object its apart of (the camera object)
        transform.position = player.transform.position + offset;
        
    }
}
