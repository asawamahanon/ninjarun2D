using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHeathpoint : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (gameObject.transform.position.y <= -5)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Home");
        }
    }
}
