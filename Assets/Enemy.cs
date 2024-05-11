using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxLife, speed, damage,cForce;
    [SerializeField] private Transform target;
    Animator animator;
    private int cLife;
    public int weight;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cLife = maxLife;
    }

    void Update()
    {
        if(target != null)
        {
            Move();
            RotatePlayer();
        }
       
    }
    void Move()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
    }
    void RotatePlayer()
    {
        Vector2 tar = target.position - transform.position;
        float angle = Mathf.Atan2(tar.y, tar.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void TakeDamage(int amount)
    {
        cLife -= amount;
        if (cLife < 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            animator.SetBool("Moving", true);
        }
          
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<Player>().TakeDamage(damage);
    }

    public bool AddForce(int amount)
    {


        return false;
    }

}
