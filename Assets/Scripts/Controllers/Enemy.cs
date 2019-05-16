using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHealth
{
    public Transform target;
    public int health = 10;

    private NavMeshAgent agent;

    public void Heal(int heal)
    {
        health += heal;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (health <= 0)
        {
            print("Enemy Illiminated");
            Destroy(gameObject);
        }
    }
}
