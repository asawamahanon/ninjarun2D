using UnityEngine;

public class playerScore : MonoBehaviour
{
    public int score;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coin")
        {
            score += 10;
            Destroy(other.gameObject);//ลบเหรียญทิ้ง
        }
    }
}
