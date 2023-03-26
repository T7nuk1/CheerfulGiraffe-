using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform startPos;
    public Transform endPos;
    private Vector3 beginPoint;
    private Vector3 endPoint;
    private float direction = -1;


    public float trapSpeed;
    public float stopDuration = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        beginPoint = startPos.position;
        endPoint = endPos.position;
    }

    void Update()
    {
        if (transform.position.y >= startPos.position.y && direction == 1)
        {
            StartCoroutine(Down());
        }
        if (transform.position.y <= endPos.position.y && direction == -1)
        {
            StartCoroutine(Up());
        }
        rb.velocity = Vector2.up * trapSpeed * direction;
    }
    private IEnumerator Down()
    {
        direction = 0;
        yield return new WaitForSeconds(stopDuration);
        beginPoint = startPos.position;
        endPoint = endPos.position;
        direction = -1;
        StopCoroutine(Down());
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator Up()
    {
        direction = 0;
        yield return new WaitForSeconds(stopDuration);
        beginPoint = endPos.position;
        endPoint = startPos.position;
        direction = 1;
        StopCoroutine(Up());
        yield return new WaitForSeconds(0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.GetComponent<HealthSystem>().TakeDamage(100);
        }
    }
}
