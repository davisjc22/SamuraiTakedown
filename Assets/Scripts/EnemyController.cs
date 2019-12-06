using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int numCollisions = 0;

    public Animator animator;

    public float speed = 1;

    public bool patrol;

    public bool moveRight = true;

    Transform player;

    public float fov = 10;

    public float startTimeBetweenShots;
    float timeBetweenShots;

    public GameObject projectile;

    public float projectileOffset;

    //Vector2 projectileStart;

    public GameObject throwPoint;

    void Start()
    {
        animator.SetBool("Patrol", patrol);

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, player.position));
        if (Vector2.Distance(transform.position, player.position) > fov)
        {
            if (patrol)
            {
                if (moveRight)
                {
                    transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                    transform.localScale = new Vector2(1, 1);

                }
            }

        }
        else
        {
            if (Vector2.Distance(transform.position, player.position) < Vector2.Distance(throwPoint.transform.position, player.position))
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, 1);
            }

            //Debug.Log("In sight!");
            if (timeBetweenShots <=0 )
            {
                //projectileStart = new Vector2(transform.position.x - projectileOffset, transform.position.y);

                
                Instantiate(projectile, throwPoint.transform.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
       
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if( trigger.gameObject.CompareTag("Turn") )
        {
            animator.SetBool("Turn", true);
            moveRight = !moveRight;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Enter Collision");
        numCollisions++;
        //if(numCollisions !=1 )
            //Destroy(this.transform.gameObject);


    }

 
}
