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

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")&& isGround == true) 
        {
            Jump();
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

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            anim.SetBool("IsJumping", false);
        }
    }
}
