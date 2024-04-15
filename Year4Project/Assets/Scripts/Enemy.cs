using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Enemy : MonoBehaviour
{
    
    //private GameManager man;
    public Vector2 attackSize;
    public Transform enemyTransform;
    public LayerMask playerMask;
    private MissedBeat mBeat;
    public bool canMove;
    public float rawDist;
    public ParticleSystem enemyParticles;
    protected int currentScale;
    protected Vector3 big = new Vector3(1.2f, 1.2f, 1.0f);
    protected Vector3 small = new Vector3(1.0f,1.0f,1.0f);
    public BoxCollider2D enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        //man = GameManager.Instance;
        currentScale = 0;
        //man.RegisterEnemy(GetInstanceID());
        //Debug.Log("GameManager" + man);
        big = new Vector3(1.2f, 1.2f, 1);
        small = new Vector3(1, 1, 1);
        enemyCollider.enabled = false;
        Invoke("StartColliding", 1f);
    }
    
    // Update is called once per frame
    void Update()
    {
        GameObject d = GameManager.Instance.defaultObject;
        if(d != null )
        {
            this.gameObject.transform.localScale = d.transform.localScale;
        }
    }
    void StartColliding()
    {
        enemyCollider.enabled = true;
    }
    /*
    void MoveAgain()
    {
        canMove = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(enemyTransform.localPosition, attackSize);
    }
    */
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WeaponZone")
        {
            WeaponController.enemyEntered = true;
            //InvokeRepeating("CheckCollision", 1f, 1f);
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "WeaponZone")
        {
            Debug.Log("Enemy exit");
            WeaponController.enemyEntered = false;
            CancelInvoke("CheckCollision");
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "WeaponZone")
        {
            WeaponController.enemyEntered = true;
            if (PlayerController.hitBeat == true && WeaponController.enemyEntered == true)
            {
                Debug.Log("This is called");
                WeaponController.durability--;
                Debug.Log(WeaponController.durability);
                if (WeaponController.durability <= 0)
                {
                    WeaponController.currentWeapon = "Null";
                }
                WeaponController.enemyEntered = false;
                Destroy(this.gameObject);
            }
        }
    }
    
    void CheckCollision()
    {
        if (PlayerController.hitBeat == true && WeaponController.enemyEntered == true)
        {
            Debug.Log("This is called");
            WeaponController.durability--;
            Debug.Log(WeaponController.durability);
            if (WeaponController.durability <= 0)
            {
                WeaponController.currentWeapon = "Null";
            }
            WeaponController.enemyEntered = false;
            Destroy(this.gameObject);
        }
    }
    */
    public void OnDestroy()
    {
        Instantiate(enemyParticles, transform.position, transform.rotation);
        GameManager.Instance.UnregisterEnemy(GetInstanceID());
    }
}
