using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// ABANDONED?
    /// </summary>

    
    public GameObject Crab;

    public float SmoothSpeed;
    private Vector3 TargetPosition;
    public Vector3 Offset;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        TargetPosition = Crab.transform.position + Offset;
        transform.position = Vector3.Lerp(transform.position, TargetPosition, SmoothSpeed);
    }



}
