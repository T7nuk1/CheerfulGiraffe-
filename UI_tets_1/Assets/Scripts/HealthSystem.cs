using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    private Animator animator;
    public float hitCooldown;

    private void Start()
    {

        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;          
        animator.SetTrigger("takeHit");
        if (health <= 0)
            StartCoroutine(Death());
    }
    private IEnumerator Death()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        animator.SetTrigger("death");
        if (transform.gameObject.name != "Player")
        {
            yield return new WaitForSeconds(0.8f);
            Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
        StopAllCoroutines();
    }
}
