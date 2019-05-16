using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage = 1;
    public abstract void Fire(Vector3 origin, Vector3 direction);
}
