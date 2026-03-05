using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HighScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    
    void Update()
    {
        
    }
}
