using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : ScriptableObject
{
    [Range(0f, 1f)] public float weighting = 1f;
    public abstract Vector3 GetForce(AI owner);
}
