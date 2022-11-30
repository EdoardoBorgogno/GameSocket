using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed; //Velocità attuale sulle 3 assi (velocity)
    }

    void Update()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if(collision.tag != "Coin") { 
        GameObject explosion = Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(explosion, 1);

        Destroy(gameObject);
        }
    }
}
