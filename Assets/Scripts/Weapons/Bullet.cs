using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public GameObject bulletHolePrefab;
    public Transform line;

    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        line.transform.rotation = Quaternion.LookRotation(rigid.velocity);
    }
    void OnCollisionEnter(Collision col)
    {
        ContactPoint contact = col.contacts[0];
        Instantiate(bulletHolePrefab, contact.point, Quaternion.LookRotation(contact.normal) *
                                                     Quaternion.AngleAxis(-90, Vector3.right));
        Destroy(gameObject);
    }
    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        line.transform.position = lineOrigin;
        rigid.AddForce(direction * speed, ForceMode.Impulse);
    }

}
