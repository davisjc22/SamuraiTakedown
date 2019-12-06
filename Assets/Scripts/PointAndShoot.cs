using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    private Vector3 target;
    private Vector2 direction;
    public Transform bone;
    public Transform firePoint;
    public LineRenderer lineRenderer;

    public GameObject player;
    public Rigidbody2D rb2d;
    public float grapple_speed = 5f;
    private float startTime;
    private float journeyLength;

    Vector3 lastMousePosition;

    private Collider2D player_base;
    bool traveling = false;
    Vector3 hitPoint;

    private Vector3 offset;            //Private variable to store the offset distance between the player and camera






    // Start is called before the first frame update
    void Start()
    {
        rb2d = player.GetComponent<Rigidbody2D>();
        offset = transform.position - player.transform.position;
        //offset.x = 0;
        
        //player_base = player.GetComponent<BoxCollider2D>().
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerSensor>().isColliding)
            {
                //Debug.Log("Button pushed!");
                traveling = true;
                startTime = Time.time;
                lastMousePosition.z = player.transform.position.z;
                //journeyLength = Vector3.Distance(player.transform.position, lastMousePosition);
                //Debug.Log("Player: "+player.transform.position + ", Mouse: " + lastMousePosition);
                Vector2 raycastdirection = new Vector2(lastMousePosition.x - firePoint.position.x, lastMousePosition.y - firePoint.position.y);
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, raycastdirection);
                hitPoint = hit.point;
                journeyLength = Vector2.Distance(firePoint.position, hitPoint);
                //Debug.DrawLine(firePoint.position, hitPoint, Color.white, 5, false);
                player.GetComponent<PlayerSensor>().resetCollisions();
                //rb2d.MovePosition(hitPoint);

            }
            if (traveling)
            {
                float distCovered = (Time.time - startTime) * grapple_speed;
                //Debug.Log("distCovered: " + distCovered);
                //Debug.Log("journeyLength: " + journeyLength);
                float fractionOfJourney = distCovered / journeyLength;
                //Debug.Log("fractionOfJourney: " + fractionOfJourney);
                Vector2 movedirection = (hitPoint - firePoint.position).normalized;
                //Debug.Log("moverdirection:" + movedirection);

                Vector2 newPosition = Vector2.MoveTowards(rb2d.position, hitPoint, fractionOfJourney * grapple_speed);
                //Debug.Log("newPosition: " + newPosition);
                if (newPosition != null)
                    rb2d.position = newPosition;
                //else
                    //Debug.Log(newPosition);
                //player.transform.position = Vector3.Lerp(player.transform.position, lastMousePosition, fractionOfJourney);
                if (player.GetComponent<PlayerSensor>().isColliding)
                {
                    traveling = false;
                }
            }
            else
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = new Vector2(-1 * (target.x - player.transform.position.x), -1 * (target.y - player.transform.position.y));
                bone.up = direction;
                Point(direction);

            }
            //transform.position = player.transform.position + offset;
        }

    }

    void Point(Vector2 direction )
    {
        Vector3 initialPosition;
        Vector3 mousePosition;

        initialPosition = firePoint.position;
        lineRenderer.SetPosition(0, initialPosition);
        lineRenderer.enabled = true;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastMousePosition = mousePosition;

        Vector2 raycastdirection = new Vector2(lastMousePosition.x - firePoint.position.x, lastMousePosition.y - firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, raycastdirection);
        Vector3 endPoint;
        endPoint = hit.point;

        lineRenderer.SetPosition(1, endPoint);

    }
}
