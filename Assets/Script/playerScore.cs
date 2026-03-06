using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
public class playerScore : MonoBehaviour
{
    public int score;
    public float timeLeft = 120 ;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI ScoreUI;
    private AudioSource au;
    public AudioClip coinClip;
    
    void Start()
    {
        if (ScoreUI != null)
        {
            ScoreUI.text = "" + score.ToString();
        }
        au = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "" + Mathf.Round(timeLeft);
        if (timeLeft < 0.1f) 
        { 
        
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coin")
        {
            score += 10; 
            if (ScoreUI != null)
            {
                ScoreUI.text = " " + score.ToString();
            }
            Destroy(other.gameObject);
            au.clip = coinClip;
            au.Play();
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else if(other.gameObject.tag == "bigcoin")
        {
            score += 100;
            if(ScoreUI != null)
            {
                ScoreUI.text = " " + score.ToString();
            }
            Destroy(other.gameObject);
            au.clip = coinClip;
            au.Play();
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
    }
}
