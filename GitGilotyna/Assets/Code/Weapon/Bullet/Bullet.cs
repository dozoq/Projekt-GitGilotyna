using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.collider.CompareTag(hittableTag))
        {
            Debug.Log("Good Tag");
            var hit = collision.collider.GetComponent<Target>();
            if (hit != null)
            {
                Debug.Log("Is Target");

                hit.TakeDamage(damage);
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
