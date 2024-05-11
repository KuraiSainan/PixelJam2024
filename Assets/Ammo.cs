using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Transform origine;
    [SerializeField] int damage,speed;
    [SerializeField] GameObject chains;
    public bool going = false;
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        if (going)
            Move();
        else
            CommingBack();
    }
   void Move()
    {
        rb.velocity = direction * speed;
    }
    void CommingBack()
    {
        direction = origine.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
        if(Vector2.Distance(transform.position,origine.position) <= 0.5)
            gameObject.SetActive(false);
    }

    public void InitialiseProj(Vector2 d, Transform ori)
    {
        direction = d.normalized;
        origine = ori;
        going = true;
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(going)
           going = false;
        if (collision.transform.CompareTag("Enemy"))
            collision.transform.GetComponent<Enemy>().TakeDamage(damage);
    }

}
