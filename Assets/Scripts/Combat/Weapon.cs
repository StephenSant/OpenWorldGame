using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = .2f;
    
    [HideInInspector]
    public bool canShoot = false;

    private float attackTimer = 0;

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer>=attackRate)
        {
            canShoot = true;
        }
    }

    public virtual void Attack()
    {
        attackTimer = 0;
        canShoot = false;
    }
}
