using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform target;
    public string targetTag = "Player";
    public float maxVelocity = 15;
    public float maxDistance = 10;
    public SteeringBehaviour[] behaviours;

    public Vector3 velocity;

    [HideInInspector]public bool hasTarget;

    protected NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        

        if (target)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
        }

        Vector3 velocity = Vector3.zero;

        foreach (var behaviour in behaviours)
        {
            float percentage = maxVelocity * behaviour.weighting;
            velocity += behaviour.GetForce(this) * percentage;
        }
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        Vector3 desiredPostition = transform.position + velocity * Time.deltaTime;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(desiredPostition, out hit, maxDistance, -1))
        {
            agent.SetDestination(hit.position);
        }
    }
}
