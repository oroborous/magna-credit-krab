using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    private float timer;
    public bool isWandering = true;

    public float wanderRadius = 50;
    public float wanderTimer = 10;
    public string myName;


    protected void Setup()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    protected void StartWandering()
    {
        isWandering = true;
    }

    protected void StopWandering()
    {
        isWandering = false;
    }

    // Update is called once per frame
    protected void Wander()
    {
        if (!isWandering)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
           // Debug.Log(myName + ": Wandering to new location" + newPos);
            Debug.DrawLine(transform.position, newPos, Color.blue, 2.5f);
            agent.SetDestination(newPos);
            agent.speed = Random.Range(5, 10);
            timer = 0;
        }
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
