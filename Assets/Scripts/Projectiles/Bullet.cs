﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles.Effects;

namespace Projectiles
{
    public class Bullet : Projectile
    {
        public float speed = 50f;
        public GameObject effectPrefab;
        public Transform line;

        private Rigidbody rigid;
        private Vector3 start, end;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            start = transform.position;
        }

        void Update()
        {
            if (rigid.velocity == Vector3.zero)
            {
                line.rotation = Quaternion.LookRotation(Vector3.forward);
            }
            else
            {
                line.rotation = Quaternion.LookRotation(rigid.velocity);
            }
        }
        void OnCollisionEnter(Collision col)
        {
            if (effectPrefab)
            {
                end = transform.position;
                ContactPoint contact = col.contacts[0];
                Vector3 bulletDir = end - start;
                Quaternion lookRotation = Quaternion.LookRotation(bulletDir);
                Quaternion rotation = lookRotation * Quaternion.AngleAxis(-90, Vector3.right);
                GameObject clone = Instantiate(effectPrefab, contact.point, rotation);
                float impactAngle = 180 - Vector3.Angle(bulletDir, contact.normal);
                clone.transform.localScale = clone.transform.localScale / (1 + impactAngle / 45);
                Effect effect = clone.GetComponent<Effect>();
                effect.damage += damage;
                effect.hitObject = col.transform;
            }
            Destroy(gameObject);
        }
        public override void Fire(Vector3 lineOrigin, Vector3 direction)
        {
            line.position = lineOrigin;
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }

    }
}