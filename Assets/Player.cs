using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxLife, speed, damage;
    private int cLife;
    Rigidbody2D rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotatePlayer();
    }
    void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction.Normalize();
        rb.velocity = direction * speed;
    }
    void RotatePlayer()
    {
       Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
       float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void TakeDamage(int amount)
    {
        cLife -= amount;
        if (cLife < 0)
            Die();
    }

    private void Die()
    {
        print("Player is Dead");
    }
}
