using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDelay : MonoBehaviour
{
    public float destroyDelay;
    float destroyTimer;
    
    void Update()
    {
        if (destroyTimer >= destroyDelay)
        {
            Destroy(gameObject);
        }
    }
}
