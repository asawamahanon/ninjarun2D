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
}
