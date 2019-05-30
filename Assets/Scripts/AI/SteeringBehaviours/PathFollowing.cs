using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "PathFollowing", menuName = "SteeringBehaviours/PathFollowing", order = 1)]
public class PathFollowing : SteeringBehaviour
{
    public float nodeRadius = 1.5f, targetRadius = 3f;
    private int currentNode = 0;
    private bool isAtTarget = false;

    private NavMeshPath path;

    public override void OnDrawGizmosSelected(AI owner)
    {
        if (path != null)
        {
            Vector3[] points = path.corners;
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 pointA = points[i];
                Vector3 pointB = points[i + 1];
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(pointA, pointB);

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(pointA, nodeRadius);
            }
        }
    }

    public override Vector3 GetForce(AI owner)
    {
        Vector3 force = Vector3.zero;

        NavMeshAgent agent = owner.agent;

        if (owner.hasTarget)
        {
            path = new NavMeshPath();
            if (agent.CalculatePath(owner.target.position, path))
            {
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    Vector3[] points = path.corners;
                    if (points.Length > 0)
                    {
                        int lastNode = points.Length - 1;
                        currentNode = Mathf.Min(currentNode, lastNode);
                        Vector3 currentPoint = points[currentNode];
                        isAtTarget = currentNode == lastNode;
                        float distanceToNode = Vector3.Distance(owner.transform.position, currentPoint);
                        if (distanceToNode< nodeRadius)
                        {
                            currentNode++;
                        }
                        force = currentPoint - owner.transform.position;
                    }
                }
            }
        }

        return force.normalized;
    }
}

