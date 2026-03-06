using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    public void StartGame() 
    { 
        SceneManager.LoadScene("SampleScene");
    }
    // --- ฟังก์ชันใหม่: โหลดหน้าแรก (เอาไว้ผูกกับปุ่ม Main Menu) ---
    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }
    // --- เพิ่มฟังก์ชันนี้เข้าไปใหม่ครับ ---
    public void ExitGame()
    {
        // บรรทัดนี้มีไว้โชว์ข้อความใน Console ให้เรารู้ว่าปุ่มทำงานแล้ว
        Debug.Log("กำลังออกจากเกม...");

        // คำสั่งปิดเกมของจริง 
        Application.Quit();
    }
}
