using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capCollider;
    private Animator animator;

    [SerializeField] private float move_speed;
    private float actual_movement;
    [SerializeField] private float jump_force;
    [SerializeField] private float velPower;
    [SerializeField] private float inAirAccel;

    private float hangTime = 0.25f;
    private float hangCounter;

    [SerializeField] private float swordAttackDuration = 0.5f;
    [SerializeField] private float fistAttackDuration = 1f;
    [SerializeField] private float knifeAttackDuration = 0.6f;
    private bool onAttack = false;

    public float fistAttackRange = 2f;
    public Vector2 knifeAttackRange;
    public float swordAttackRange = 2.3f;
    

    [SerializeField] private LayerMask groundLayer;
    public Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        actual_movement = move_speed; 
    }
    private void Update()
    {
        if (Input.GetKey("a") && actual_movement > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey("d") && actual_movement > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Grounded())
        {
            animator.SetBool("isJumping", false);
            hangCounter = hangTime;
        } else
        {
            animator.SetBool("isJumping", true);
            hangCounter -= Time.deltaTime;
        }
        animator.SetFloat("yVelocity", rb.velocity.y);

        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * move_speed, rb.velocity.y);

        if (Input.GetKeyDown("space") && hangCounter > 0f)
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jump_force);         
        }
        if (Input.GetKeyUp("space") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.F) && !onAttack)
        {
            StartCoroutine(Fist_Attack());           
        }
        if (Input.GetKeyDown(KeyCode.G) && !onAttack)
        {
            StartCoroutine(Knife_Attack());
        }
        if (Input.GetKeyDown(KeyCode.H) && !onAttack)
        {
            StartCoroutine(Sword_Attack());
        }

    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        float targetSpeed = Input.GetAxisRaw("Horizontal") * actual_movement;
        float speedDif = targetSpeed - rb.velocity.x;
        animator.SetFloat("Speed", Mathf.Abs(targetSpeed));
        float accelRate = 7;
        if (hangCounter < hangTime)
            accelRate *= inAirAccel;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }

    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(capCollider.transform.position, Vector2.down, capCollider.size.y / 2 * 5 + 0.2f, groundLayer);
        return hit.collider != null;
    }

    private IEnumerator Fist_Attack()
    {
        animator.SetTrigger("fist_attack");
        onAttack = true;
        actual_movement = 0f;

        //fist attack hits region
        yield return new WaitForSeconds(fistAttackDuration / 2);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, fistAttackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<HealthSystem>().TakeDamage(5);
        }

        yield return new WaitForSeconds(fistAttackDuration / 2);
        /////////////////////////
        onAttack = false;
        actual_movement = move_speed;
        StopCoroutine(Fist_Attack());
    }
    private IEnumerator Knife_Attack()
    {
        animator.SetTrigger("knife_attack");
        onAttack = true;
        actual_movement = 0f;
        
        //knife attack hits region
        yield return new WaitForSeconds(knifeAttackDuration / 2);

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, knifeAttackRange, 0,  enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<HealthSystem>().TakeDamage(2);
        }

        yield return new WaitForSeconds(knifeAttackDuration / 2);
        /////////////////////////
        onAttack = false;
        actual_movement = move_speed;
        StopCoroutine(Knife_Attack());
    }
    private IEnumerator Sword_Attack()
    {
        animator.SetTrigger("sword_attack");
        onAttack = true;
        actual_movement = 0f;

        //sword attack hits region
        yield return new WaitForSeconds(swordAttackDuration/2);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, swordAttackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<HealthSystem>().TakeDamage(3);
        }

        yield return new WaitForSeconds(swordAttackDuration / 2);
        /////////////////////////
        onAttack = false;
        actual_movement = move_speed;
        StopCoroutine(Sword_Attack());
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, fistAttackRange);
        Gizmos.DrawWireSphere(attackPoint.position, swordAttackRange);
        Gizmos.DrawWireCube(attackPoint.position, knifeAttackRange);
    }
}
