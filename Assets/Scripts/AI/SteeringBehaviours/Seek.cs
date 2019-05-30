using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seek", menuName = "SteeringBehaviours/Seek", order = 1)]
public class Seek : SteeringBehaviour
{
    public float stoppingDistance = 1;

    public override void OnDrawGizmosSelected(AI owner)
    {
        Gizmos.color = Color.blue;
        float distance = Vector3.Distance(owner.target.position, owner.transform.position);
        Gizmos.DrawWireSphere(owner.transform.position, distance - stoppingDistance);
    }

    public override Vector3 GetForce(AI owner)
    {
        Vector3 force = Vector3.zero;


        float distance = Vector3.Distance(owner.transform.position, owner.target.position);
        // If AI
        if (distance > stoppingDistance)
        {
            if (owner.hasTarget)
            {
                force += owner.target.position - owner.transform.position;
            }
        }

        return force.normalized;
    }
}
