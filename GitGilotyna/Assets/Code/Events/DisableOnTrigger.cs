using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool utilizeThisScript = true;
    [SerializeField] private bool utilizeThisObject = true;
    [SerializeField] private bool utilizeTargetObject = true;
    public void DisableObject()
    {
        if(!gameObject.activeSelf) return;
        if(utilizeTargetObject) Destroy(_gameObject);
        else _gameObject.SetActive(false);
        if (utilizeThisObject) Destroy(gameObject);
        else if(utilizeThisScript) Destroy(this);
    }
}
