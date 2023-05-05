using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterAndExit : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider2D> onTriggerStay;
    [SerializeField] private UnityEvent<Collider2D> onTriggerExit;
    [SerializeField] private string triggerTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(triggerTag))
            onTriggerEnter.Invoke(other);
        Debug.Log("After Trigger");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag(triggerTag))
            onTriggerStay.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(triggerTag))
            onTriggerExit.Invoke(other);
    }
}
