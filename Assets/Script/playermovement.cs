using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isKnockbacked = false;

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
            // ถ้ากระเด็นอยู่ จะไม่ให้กระโดด
            if (!isKnockbacked&&(isGround || jumpCount < maxJumps))
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
        // --- เพิ่มเงื่อนไข: ถ้าไม่ได้กระเด็นอยู่ ถึงจะยอมให้ผู้เล่นบังคับซ้ายขวาได้ ---
        if (!isKnockbacked) 
        {
            rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
        }
            
    }
    // ฟังก์ชันใหม่ที่เอาไว้เรียกใช้ตอนโดนชน
    public void ApplyKnockback(Vector2 enemyPosition)
    {
        isKnockbacked = true;

        // เช็คว่าศัตรูอยู่ทางซ้ายหรือขวา เพื่อให้กระเด็นไปทิศตรงข้าม
        int knockbackDir = transform.position.x < enemyPosition.x ? -1 : 1;

        // หยุดความเร็วเดิมก่อนกระเด็น
        rb.linearVelocity = Vector2.zero;

        // ใส่แรงกระแทก (กระเด็นถอยหลังและเด้งขึ้นบนนิดหน่อย)
        rb.AddForce(new Vector2(knockbackDir * 8f, 5f), ForceMode2D.Impulse);

        // ให้เวลากระเด็น 0.3 วินาที แล้วกลับมาบังคับได้ปกติ
        Invoke("ResetKnockback", 0.3f);
    }

    private void ResetKnockback()
    {
        isKnockbacked = false;
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
        if (collision.gameObject.CompareTag("level"))
        {
            // ใช้ SceneManager แทน Application ครับ
            SceneManager.LoadScene("Home");
        }
    }
}
