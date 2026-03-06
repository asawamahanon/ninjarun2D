using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHeathpoint : MonoBehaviour
{
    public int live = 3;
    // สร้างกล่องเก็บ GameObject แบบ Array (ใส่ได้หลายชิ้น) เพื่อเก็บรูปหัวใจที่แสดงจำนวนชีวิต
    public GameObject[] life;
    // --- ตัวแปรสำหรับสถานะอมตะ ---
    public bool isInvincible = false;
    public float invincibilityTime = 1.5f;
    void Start()
    {
    }

    void Update()
    {
        if (gameObject.transform.position.y <= -5)
        {
            Die();
        }
    }
    public void takedmg(Vector2 enemyPos) 
    {
        // ถ้าอยู่ในสถานะอมตะ ให้เด้งออกจากฟังก์ชันนี้เลย ไม่โดนดาเมจ
        if (isInvincible) return;
        live -= 1;
        // --- ส่วนที่เพิ่มเข้ามาใหม่สำหรับจัดการ UI หัวใจ ---
        // เช็คก่อนว่ายังมีหัวใจให้ปิดอยู่ไหม (กัน Error)
        if (live >= 0 && live < life.Length)
        {
            // สั่งปิดการแสดงผลรูปหัวใจ
            // เช่น ถ้า lives เหลือ 2 จะไปปิด hearts ตำแหน่งที่ 2 (ดวงที่ 3)
            life[live].SetActive(false);
        }
        // --------------------------------
        Debug.Log("Player took damage! Remaining lives: " + live);
        if (live <= 0)
        {
            Die();
        }
        else 
        {
            // 1. สั่งให้กระเด็น โดยส่งตำแหน่งศัตรูไปให้คำนวณ
            GetComponent<playermovement>().ApplyKnockback(enemyPos);

            // 2. เริ่มเปิดโหมดอมตะ 1.5 วิ
            StartCoroutine(InvincibilityRoutine());
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("gameover");
    }

    // --- ระบบจับเวลาและทำตัวกระพริบ ---
    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // ทำให้ตัวกระพริบระยิบระยับเป็นเวลา 1.5 วินาที
        for (float i = 0; i < invincibilityTime; i += 0.2f)
        {
            sr.color = new Color(1, 1, 1, 0.5f); // จางลงครึ่งนึง
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1, 1, 1, 1f);   // กลับมาสว่างปกติ
            yield return new WaitForSeconds(0.1f);
        }

        // ครบเวลา ยกเลิกอมตะ
        sr.color = new Color(1, 1, 1, 1f);
        isInvincible = false;
    }
}
