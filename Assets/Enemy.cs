using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxLife, speed, damage;
    [SerializeField] private Transform target;
    private int cLife;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cLife = maxLife;
    }

    void Update()
    {
        if(target != null)
        Move();
    }
    void Move()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
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
            target = collision.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<Player>().TakeDamage(damage);
    }

}
