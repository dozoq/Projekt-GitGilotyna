using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnPickup : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public void EnableObject()
    {
        _gameObject.SetActive(true);
    }
}
