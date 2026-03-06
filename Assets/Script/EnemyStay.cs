using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStay : MonoBehaviour
{
    // ตั้งค่าความสูงที่ถือว่าเป็นการ "เหยียบ" (ปรับเลขได้ใน Unity)
    public float stompOffset = 0.5f; // ระยะห่างที่ใช้ในการตรวจสอบการกระโดดทับ
    // แรงเด้งดึ๋งตอนผู้เล่นเหยียบศัตรูตาย
    public float bounceForce = 0.5f; // แรงที่ใช้ในการกระโดดกลับเมื่อทับศัตรู

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // เช็คว่าคนที่มาชนมี Tag เป็น "Player" ใช่หรือไม่?
        if (collision.gameObject.CompareTag("Player"))
        {
            // เช็คว่าผู้เล่นอยู่ "สูงกว่า" ศัตรู + ระยะ Offset ใช่หรือไม่?
            if (collision.transform.position.y > transform.position.y + stompOffset)
            {
                // -- กรณี: เหยียบหัวสำเร็จ! --

                // 1. ทำให้ผู้เล่นเด้งกระโดดขึ้นไปเหมือนมาริโอ้
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    // ใช้ linearVelocity เซ็ตความเร็วแกน Y พุ่งขึ้นไปตรงๆ
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
                }

                // 2. ทำลายศัตรูทิ้ง
                Destroy(gameObject);
            }
            // -- กรณี: ชนด้านข้าง (ผู้เล่นอยู่ต่ำกว่าหรือระดับเดียวกัน) --
            else
            {
                // ดึง Script "playerHeathpoint" ของผู้เล่นมา
                playerHeathpoint hp = collision.gameObject.GetComponent<playerHeathpoint>();

                // สั่งให้ผู้เล่นลดเลือดตัวเอง
                if (hp != null)
                {
                    // เปลี่ยนจาก hp.TakeDamage(); เป็น...
                    hp.takedmg(transform.position);
                } 
            }
        }
    }
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
}
