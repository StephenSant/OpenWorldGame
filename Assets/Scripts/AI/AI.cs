using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class AI : MonoBehaviour
{
    public bool hasTarget;
    [ShowIf("hasTarget")]public Transform target;
    [Tag]public string targetTag = "Player";
    public float maxVelocity = 15;
    public float maxDistance = 10;
    public SteeringBehaviour[] behaviours;
    [ReadOnly]public Vector3 velocity;
    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;

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

    private void OnDrawGizmosSelected()
    {
        Vector3 desiredPosition = transform.position + velocity * Time.deltaTime;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(desiredPosition, .1f);

        // Render all behaviours
        foreach (var behaviour in behaviours)
        {
            behaviour.OnDrawGizmosSelected(this);
        }
    }
}
