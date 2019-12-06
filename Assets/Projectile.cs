using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;

    Rigidbody2D rb;

    private Vector2 movementVector;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        movementVector = player.position - transform.position;

        movementVector.Normalize();

        rb = transform.GetComponent<Rigidbody2D>();

        rb.AddForce(movementVector * speed);
    }

    void Update()
    {
        //rb.AddForce(movementVector);
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //Vector2 raycastdirection = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastdirection);
        //transform.position = Vector2.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);

        //if( transform.position.x == target.x && transform.position.y == target.y)
        //{
        //    DestroyProjectile();
        //}
    }

    void OnTriggerEnter2d( Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.tag);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
