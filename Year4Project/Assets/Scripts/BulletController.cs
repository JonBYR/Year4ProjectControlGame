using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5f;
    private Transform playerPos;
    bool negative = false;

    private void Awake()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        float horizontal = playerPos.position.x - this.transform.position.x;
        float vertical = playerPos.position.y - this.transform.position.y;
        if (Mathf.Abs(horizontal) < Mathf.Abs(vertical)) //horizontal should be of negligable distance
        {
            if (vertical < 0) negative = true;
            else negative = false;
        }
        else if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            if(horizontal < 0)
            {
                negative = true;
            }
            else
            {
                negative = false;
            }
        }
        if(negative && (Mathf.Abs(horizontal) < Mathf.Abs(vertical)))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
        else if(negative && (Mathf.Abs(horizontal) > Mathf.Abs(vertical)))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
        else if(!negative && (Mathf.Abs(horizontal) < Mathf.Abs(vertical)))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
        else if (!negative && (Mathf.Abs(horizontal) > Mathf.Abs(vertical)))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TriggerZone" || collision.gameObject.name.Contains("Mage"))
        {
            Debug.Log("Called");
            return;
        }
        else if (collision.gameObject.tag == "Player") 
        {
            PlayerHealth.TakeDamage();
            Destroy(this.gameObject); 
        }
    }
}
