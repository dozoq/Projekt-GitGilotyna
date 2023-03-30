using System;
using System.Collections;
using System.Collections.Generic;
using Code.Mobs;
using Code.Utilities.Extends;
using UnityEngine;

public class AcidSpoil : MonoBehaviour
{
    [SerializeField] private List<string> targetTags;
    [SerializeField] private float damage;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float tickTime;
    private List<Target> _avaibleTargets = new List<Target>();
    private float _lifeTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareListOfTags(targetTags) && other.gameObject.GetComponent<Target>() != null)
        {
            _avaibleTargets.Add(other.GetComponent<Target>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareListOfTags(targetTags) && other.gameObject.GetComponent<Target>() != null)
        {
            _avaibleTargets.Remove(other.GetComponent<Target>());
        }
    }

    private void Start()
    {
        StartCoroutine(HandleLifetimeAndDoDamage());
    }

    private IEnumerator HandleLifetimeAndDoDamage()
    {
        while (_lifeTime < maxLifeTime)
        {
            _lifeTime += tickTime;
            foreach (var target in _avaibleTargets)
            {
                target.TakeDamage(damage);
            }

            yield return new WaitForSeconds(tickTime);
        }
        Destroy(gameObject);
    }
}
