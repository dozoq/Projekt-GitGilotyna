using System;
using Code.Mobs;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string hittableTag;
    private int damage;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    public void Initialize(string tag, int damage)
    {
        hittableTag = tag;
        this.damage = damage;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(hittableTag))
        {
            var hit = collider.GetComponent<Target>();
            if (hit != null)
            {
                hit.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < 1)
        {
            Destroy(gameObject);
        }
    }
}
