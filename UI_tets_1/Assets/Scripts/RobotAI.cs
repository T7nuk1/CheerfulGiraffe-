using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPos;
    
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 startPos;
    [SerializeField] private float patrolAreaX;
    [SerializeField] private float robotSpeed;

    private bool inAttack = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }
    private void Update()
    {
        if (transform.position.x > startPos.x + patrolAreaX)
        {
            transform.rotation = new Quaternion(0, 180, 0, transform.rotation.w);
        }
        if (transform.position.x < startPos.x - patrolAreaX)
        {
            transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
        }
        if (!inAttack)
            rb.velocity = new Vector2(robotSpeed, rb.velocity.y) * transform.right;

        if (!inAttack)
        {  
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 15f, player);
            if (hit.collider != null)
            {
                StartCoroutine(AttackPreparation());
            }
        }        
            
    }
    private IEnumerator AttackPreparation()
    {
        animator.SetTrigger("seePlayer");
        inAttack = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(3f);
        yield return Instantiate(bullet, shootPos.position, transform.rotation);
        inAttack = false;
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
        StopCoroutine(AttackPreparation());
    }

}
