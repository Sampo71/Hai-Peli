using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float currentSpeed = 6f;
    public int maxHealth = 3;
    private int currentHealth = 0;
    private Rigidbody2D body;
    public Transform playerTransform;
    public float attackRange = 8f;
    public int attackPower = 1;
    public float dashSpeed = 24f;
    public float dashDuration = 0.75f;
    public float attackCooldown = 2f;
    private bool isDashing = false;
    private float attackTimer = 0f;
    Vector2 direction;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable(){
        currentHealth = maxHealth;
        isDashing = false;
    }

    void OnDisable(){
        StopAllCoroutines();
    }

    void FixedUpdate()
    {
        Move();
        Attack();
    }

    private void Attack()
    {
        if(playerTransform == null)
        {
           return;
        }
        if(attackTimer > 0){
            attackTimer -= Time.fixedDeltaTime;
        }
        else if(!isDashing && Vector2.Distance(transform.position, playerTransform.position) < attackRange)
        {
            StartCoroutine(DashAttack());
        }
    }

    IEnumerator DashAttack(){
       isDashing = true;
       float startTime = Time.time;

       while(Time.time < startTime + dashDuration){
          body.velocity = direction * dashSpeed;

          yield return null;
       }
       body.velocity = Vector2.zero;
       isDashing = false;
       attackTimer = attackCooldown;
    }

    private void Move()
    {
       if(playerTransform == null)
       {
          GetPlayer();
          return;
       }

       if(isDashing == true){
        return;
       }

       direction = (playerTransform.position - transform.position).normalized;
       body.MovePosition(body.position + direction * currentSpeed * Time.fixedDeltaTime);
    }

    void GetPlayer()
    {
        playerTransform = GameManager.Instance.getPlayer.transform;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die()
    {
        EnemyPoolManager.Instance.ReturnEnemy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(isDashing == true && other.CompareTag("Player")){  
            other.GetComponent<IDamageable>().TakeDamage(1);          
        }
    }
}
