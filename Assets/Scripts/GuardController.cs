using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : WanderingAI
{
    public float lookRadius = 10f;
    private Transform crab;
    private Claw leftClaw, rightClaw;
    private GameManager gm;

    private float dangerTimer = 0;
    private float danger = 4f;

    public bool isChasing;

    void Start()
    {
        Setup();
        gm = FindObjectOfType<GameManager>();
        crab = GameObject.Find("Crab").transform;
        leftClaw = GameObject.Find("L_Claw").gameObject.GetComponent<Claw>();
        rightClaw = GameObject.Find("R_Claw").gameObject.GetComponent<Claw>();
    }

    void Update()
    {
        if (CrabHasItem() && CrabInFront() && HasLineOfSight())
        {
            Debug.Log("GuardController: Heading toward player");
            gm.PlayWarning();
            StopWandering();
            agent.SetDestination(crab.position);
            agent.speed = 10;

            if (dangerTimer <= 0)
            {
                gm.spawnSprite(GameManager.DANGER, crab);
                dangerTimer = danger;
            }

            dangerTimer -= Time.deltaTime;
            isChasing = true;
        }

        isChasing = false;
        StartWandering();
        Wander();
        gm.StopWarning();
    }

    private bool CrabHasItem()
    {
        return leftClaw.HasItem() || rightClaw.HasItem();
    }

    private bool HasLineOfSight()
    {
        RaycastHit hit;
        Vector3 directionToTarget = crab.position - transform.position;

        if (Physics.Raycast(transform.position, directionToTarget, out hit, lookRadius))
        {
            //Debug.Log(hit.transform.name);

            if (hit.transform.name.StartsWith("Crab"))
            {
                Debug.DrawRay(transform.position, directionToTarget, Color.red);
                return true;
            }
        }

        return false;
    }

    private bool CrabInFront()
    {
        Vector3 directionToTarget = transform.position - crab.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if (Mathf.Abs(angle) > 115 && Mathf.Abs(angle) < 245)
        {
            Debug.DrawLine(transform.position, crab.position, Color.green);
            return true;
        }

        Debug.DrawLine(transform.position, crab.position, Color.yellow);
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (isChasing && other.gameObject.tag == "Claw")
        {
            gm.isPlaying = false;
        }
    }
}
