using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomTimeInvoker : MonoBehaviour
{
    [SerializeField] private float repeatInvokeTime = 1f;
    [SerializeField, Range(0,100)] private float chance = 0f;
    [SerializeField] private UnityEvent onInvoke;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RandomInvoking", 0, repeatInvokeTime);
    }

    private void RandomInvoking()
    {
        var random = Random.Range(0f, 100f);
        if(random <= chance)
            onInvoke.Invoke();
    }
}
