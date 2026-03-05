using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerHeathpoint : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.position.y <= -5) 
        {
            Destroy(gameObject);
        }
    }
}
