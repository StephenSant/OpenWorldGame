using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flee", menuName = "SteeringBehaviours/Flee", order = 1)]
public class Flee : SteeringBehaviour
{
    public float stoppingDistance = 1;
    public override Vector3 GetForce(AI owner)
    {
        Vector3 force = Vector3.zero;

        if (owner.target)
        {
            force += owner.target.position - owner.transform.position;
        }

        return force.normalized;
    }
}
