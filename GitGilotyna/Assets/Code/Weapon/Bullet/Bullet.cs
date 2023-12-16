using System;
using Code.Mobs;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string hittableTag;
    private int damage;
    private Rigidbody2D rb;
    private float startingVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingVelocity = rb.velocity.magnitude;
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
                hit.TakeDamage(damage, MapVelocityToHitDirection());
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public Vector3 MapVelocityToHitDirection()
    {
        float angle =
            Mathf.Cos(rb.velocity.x / Mathf.Pow(rb.velocity.x,2) + Mathf.Pow(rb.velocity.y,2))*360;
        Debug.Log(angle);

        if (rb.velocity.x > 0) return new Vector3(0, 0, angle);
        return new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < 1)
        {
            Destroy(gameObject);
        }
    }
}
