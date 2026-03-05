using UnityEngine;
using System.Collections;
using System.Collections.Generic;   

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) 
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
            transform.position = new Vector3(x, y, transform.position.z);

        }
    }
}
