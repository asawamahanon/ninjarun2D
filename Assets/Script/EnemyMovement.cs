using UnityEngine;
using System.Collections;   
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement; // Add this using directive

public class EnemyMovement : MonoBehaviour
{
    public int speed = 1;
    public int xmove = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        Vector2 direction = new Vector2(xmove, 0);

        
        Vector2 startPos = new Vector2(transform.position.x + (xmove * 0.6f), transform.position.y);

        
        Debug.DrawRay(startPos, direction * 2f, Color.red);

        
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 2f);

        
        rb.linearVelocity = new Vector2(xmove * speed, rb.linearVelocity.y);

        
        if (hit.collider != null)
        {
            
            if (!hit.collider.isTrigger)
            {
                Flip(); // สั่งหันหลัง


                if (hit.collider.CompareTag("Player"))
                {
                    Destroy(hit.collider.gameObject);
                    SceneManager.LoadScene("Home");
                }
            }
        }
    }

    private void Flip()
    {
        
        if (xmove > 0)
        {
            xmove = -1;
        }
        else
        {
            xmove = 1;
        }

        
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }
}