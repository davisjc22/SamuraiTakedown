using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSensor : MonoBehaviour
{
    int numCollisions = 0;

    int count = 0;
    int numEnemies;

    public GameObject winMenu;

    public GameObject loseMenu;

    public TextMeshProUGUI enemiesRemaining;
    public GameObject enemies;
    public AudioClip kick;

    private AudioSource source;

    public bool isColliding
    {
        get
        {
            return numCollisions > 0;
        }
    }

    // Count how many colliders are overlapping this trigger.
    // If desired, you can filter here by tag, attached components, etc.
    // so that only certain collisions count. Physics layers help too.
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    //Debug.Log("Enter Collision");
    //    numCollisions++;
    //}



    public void resetCollisions()
    {
        numCollisions = 0;
    }

    //void OnCollisionExit2D(Collision2D col)
    //{
    //    Debug.Log("Exit Collision");
    //    numCollisions--;

    //}

    void Start()
    {
        source = GetComponent<AudioSource>();
        numEnemies = enemies.transform.childCount;
        //Debug.Log(numEnemies);
        enemiesRemaining.SetText("Enemies Remaining: " + (numEnemies-count));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        numCollisions++;
        //Debug.Log("triggered");
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.transform.gameObject);
            //Debug.Log("Hit the Samurai");
            //other.transform.gameObject.SetActive(false);
            source.PlayOneShot(kick);
            //Debug.Log("Hit one");
            count = count + 1;
            SetCountText();
        }

        else if (other.transform.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Take Damage!");
            Time.timeScale = 0f;
            loseMenu.SetActive(true);
        }

    }

    void SetCountText()
    {
        enemiesRemaining.SetText("Enemies Remaining: " + (numEnemies - count));
        if (count >= numEnemies)
        {
            winMenu.SetActive(true);
        }
    }
}
