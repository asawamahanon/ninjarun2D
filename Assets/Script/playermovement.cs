using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class playermovement : MonoBehaviour
{
    public int speed = 10;
    private Rigidbody2D rb;
    public int jumpForce = 10;
    public bool isGround;
    public float moveX;
    private Animator anim;
    private bool mirror;
    private AudioSource au;
    public AudioClip jumpClip;
    public int maxJumps = 2;
    private int jumpCount = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) 
        {
            if (isGround || jumpCount < maxJumps)
            {
                Jump();
            }
        }
        if (!isGround) 
        {
            anim.SetBool("IsJumping", true);
        }
        if (moveX != 0 && isGround)
        {
            anim.SetBool("IsRunning", true);
        }
        else 
        { 
            anim.SetBool("IsRunning", false);
        }
        if (moveX > 0.0f && mirror == true)
        {
            FlipPlayer();
        }
        else if (moveX < 0.0f && mirror == false) 
        {
            FlipPlayer();
        }
            rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    private void FlipPlayer()
    {
        mirror = !mirror;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Jump()
    {
        rb.AddForce( Vector2.up * jumpForce);
        isGround = false;
        jumpCount++;
        au.clip = jumpClip;
        au.Play();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            jumpCount = 0;
            anim.SetBool("IsJumping", false);
        }
    }
}
