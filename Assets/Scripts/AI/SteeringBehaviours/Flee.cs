using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flee", menuName = "SteeringBehaviours/Flee", order = 1)]
public class Flee : SteeringBehaviour
{
    public float stoppingDistance = 1;

    public override void OnDrawGizmosSelected(AI owner)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(owner.target.position, stoppingDistance);
    }

    public override Vector3 GetForce(AI owner)
    {
        Vector3 force = Vector3.zero;

        float distance = Vector3.Distance(owner.transform.position, owner.target.position);
        if (distance < stoppingDistance)
        {
            if (owner.hasTarget)
            {
                force += owner.transform.position - owner.target.position;
            }
        }
        return force.normalized;
    }
}
