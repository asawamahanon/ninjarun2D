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
    public float stompOffset = 0.5f; // ระยะห่างที่ใช้ในการตรวจสอบการกระโดดทับ
    public float bounceForce = 0.5f; // แรงที่ใช้ในการกระโดดกลับเมื่อทับศัตรู

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 direction = new Vector2(xmove, 0);
        Vector2 startPos = new Vector2(transform.position.x + (xmove * 1.5f), transform.position.y + 0.5f);
        Debug.DrawRay(startPos, direction * 2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 2f);

        rb.linearVelocity = new Vector2(xmove * speed, rb.linearVelocity.y);

        if (hit.collider != null)
        {
            // เช็คว่าชนกำแพง (ที่ไม่ใช่ Player) ถึงจะหันหลังกลับ                          // เช็คว่าชนกำแพง (ที่ไม่ใช่ Player) และ (ไม่ใช่ตัวเอง) ถึงจะหันหลังกลับ
            if (!hit.collider.isTrigger && !hit.collider.CompareTag("Player") && hit.collider.gameObject != gameObject)
            {
                Flip(); // สั่งหันหลัง
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
    // เพิ่มฟังก์ชันตรวจจับการชนและเหยียบหัวไว้ตรงนี้
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // เช็คว่าผู้เล่นอยู่สูงกว่า = เหยียบหัว
            if (collision.transform.position.y > transform.position.y + stompOffset)
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
                }
                Destroy(gameObject); // ศัตรูตาย
            }
            else // ถ้าชนด้านข้าง
            {
                playerHeathpoint hp = collision.gameObject.GetComponent<playerHeathpoint>();
                if (hp != null)
                {
                    hp.takedmg(transform.position); // ผู้เล่นโดนดาเมจ
                }
            }
        }

    }
}