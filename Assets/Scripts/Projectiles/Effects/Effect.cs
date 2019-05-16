using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public abstract class Effect : MonoBehaviour
    {
        public float effectRate = 1f;
        public int damage = 1;
        public GameObject visualEffectPrefab;
        [HideInInspector] public Transform hitObject;
        private float effectTimer = 0f;

        protected virtual void Start()
        {
            GameObject clone = Instantiate(visualEffectPrefab, hitObject.transform);
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
        }

        protected virtual void Update()
        {
            effectTimer += Time.deltaTime;
            if (effectTimer >= 1f / effectRate)
            {
                RunEffect();
            }
        }
        public abstract void RunEffect();
    }
}
