using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public bool Moves = true;
    public bool DestroySelfOnHit = true;
    public bool DoesDamage = true;
    public bool CanDestroyCrackedWalls = false;

    public GameObject LastingHitEffect;

    private bool HitEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Moves)
        {
            rb.velocity = transform.TransformDirection(Vector2.right * 15);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet" && collision.tag != "Player") {
            HitEnemy = false;
            if (DoesDamage)
            {
                if (collision.tag == "Enemy")
                {
                    HitEnemy = true;
                    Destroy(collision.gameObject); // destroy the enemy
                }

                if (collision.GetComponent<DynamicWall>() && CanDestroyCrackedWalls)
                {
                    collision.GetComponent<DynamicWall>().DestroyWithViolence();
                }
            }
            

            Hit();

        }
    }

    public void Hit()
    {
        if (LastingHitEffect)
        {
            Debug.Log("spawning lasing hit effect");
            Instantiate(LastingHitEffect, transform.position, transform.rotation);
        }


        // this has to happen last
        if (DestroySelfOnHit)
        {
            Destroy(gameObject); // destroy myself regardless of what i hit
        }
    }
}
