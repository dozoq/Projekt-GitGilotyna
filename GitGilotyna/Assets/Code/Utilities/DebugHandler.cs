using System.Collections;
using System.Collections.Generic;
using Code.General;
using UnityEngine;

public class DebugHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerRef;
    // Start is called before the first frame update
    void Start()
    {
        if(!FindObjectOfType<GameManager>() )
            Instantiate(gameManagerRef);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
