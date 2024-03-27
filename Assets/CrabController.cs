using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public GameObject CrabMesh;

    public Transform camPivot;
    public Transform cam;
    private Quaternion homePivot;
    float camRotate;
    public float speed = 10;

    [Range(0.0f, 10.0f)]
    public float clawRange;

    Vector2 input;

    public Animator anim;

    public bool IsPlaying = true;
    private GameManager gm;

    private void Start()
    {
        homePivot = transform.rotation;
        gm = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        IsPlaying = gm.isPlaying; //check if isPlaying

        Claw[] claws = GameObject.FindObjectsOfType<Claw>(); ;
        foreach (Claw curr in claws)
        {
            curr.dist = clawRange;
            if (!IsPlaying)
            {
                curr.DropItem();
            }
        }

        float horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(vertical);

       

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;




        camRotate += Input.GetAxis("Mouse X") * Time.deltaTime*100; //Get Rotation for Camera based on Mouse Movement
        
        camPivot.rotation = Quaternion.Lerp(Quaternion.Euler(0, camRotate, 0), Quaternion.Euler(-horizontal * 35, camRotate, vertical*20), 0.08f); //Rotate Camera

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
       
        Vector3 camF = cam.forward; //Forward Cam
        Vector3 camR = cam.right; //Right Cam

        //angle of camera without veritcals then Normalize
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized; 
        camR = camR.normalized;

        if (IsPlaying)
        {
            //Move Character
            transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * 10;

            if (horizontal == 0 && vertical == 0)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
            CrabMesh.transform.rotation = Quaternion.Euler(0, camPivot.rotation.y + camRotate, 0); //Rotate Mesh 
        }


        //Lock and Unlock Cursor
        if (Input.GetKey(KeyCode.Escape) && Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        

    }

}
