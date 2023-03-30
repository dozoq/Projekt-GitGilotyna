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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(hittableTag))
        {
            var hit = collision.collider.GetComponent<Target>();
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
