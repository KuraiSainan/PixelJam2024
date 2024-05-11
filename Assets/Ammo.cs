using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ammo : MonoBehaviour
{
    private Transform origine;
    [SerializeField] int damage,speed,force;
    public int resitance;
    [SerializeField] Transform hook;
    [SerializeField] GameObject chainOff;
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    ObjectPool pool;
    Chain chainc;
     public bool commingBack = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pool = ObjectPool.instance;
        chainc = GetComponent<Chain>();
        chainc.enabled = false;
    }



    // Update is called once per frame
    void Update()
    {      if(!commingBack)
             Move();
    }
   void Move()
    {
        rb.velocity = direction * speed;
    }
    //void CommingBack()
    //{
    //    direction = origine.position - transform.position;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    //    direction.Normalize();
    //    rb.velocity = direction * speed;
    //    if(Vector2.Distance(transform.position,origine.position) <= 0.5)
    //        gameObject.SetActive(false);
    //}

    public void InitialiseProj(Vector2 d, Transform ori)
    {
        float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        commingBack = false;
        direction = d.normalized;
        origine = ori;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        GameObject chainn = pool.GetPooledObject(chainOff);
        chainn.transform.position = origine.position;
        Chain chana = chainn.GetComponent<Chain>();
        chana.next = transform;
        chana.before = ori;
        chainc.before = chana.transform;
        chainn.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy en = collision.GetComponent<Enemy>();
            en.TakeDamage(damage);
            en.AddForce(force);
            resitance = en.weight - force;
            chainc.next = collision.transform;
            chainc.first = true;
        }
        if (!commingBack)
        {
            commingBack = true;
            StartCoroutine(shitWait());
        }
    }

    IEnumerator shitWait()
    {
        yield return new WaitForSeconds(0.1f);
        chainc.enabled = true;
        chainc.ChainCommingBack(resitance);
    }
}
