using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public string hitTag;
    public UnityEvent onEnter, onStay, onExit;

    private void Reset()
    {
        // Does the object have a collider?
        Collider col = GetComponent<Collider>();
        if (col)
        {
            // Forces the attached collider to be a trigger
            col.isTrigger = true;
        }
        else
        {
            // Debug LogWarning 
            Debug.LogWarning("The GameObject named '" + name + "' does not have a collider, and will not work with OnTriggerEvent");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == hitTag || hitTag == "")
        {
            onEnter.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == hitTag || hitTag == "")
        {
            onStay.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == hitTag || hitTag == "")
        {
            onExit.Invoke();
        }
    }
}
