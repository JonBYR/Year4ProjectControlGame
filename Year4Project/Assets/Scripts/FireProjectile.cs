using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    bool fired;
    public GameObject bullet;
    //public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!fired)
            {
                fired = true;
                GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
                Debug.Log("Firing projectile");
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Lost");
            fired = false;
        }
    }
}
