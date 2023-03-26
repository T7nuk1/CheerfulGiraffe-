using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(bulletSpeed, rb.velocity.y) * transform.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(2);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        { 
            Destroy(this.gameObject);
        }


    }
}
