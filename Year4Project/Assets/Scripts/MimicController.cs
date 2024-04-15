using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicController : Enemy
{
    /// <summary>
    /// private GameManager man;
    /// </summary>
    private GameManager man;
    private PlayerController player;
    public LayerMask walls;
    // Start is called before the first frame update
    void Start()
    {
        man = GameManager.Instance;
        man.RegisterEnemy(GetInstanceID());
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        GameObject d = GameManager.Instance.defaultObject;
        if (d != null)
        {
            this.gameObject.transform.localScale = d.transform.localScale;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerController.moving == true)
        {
            if (player.horizontal)
            {
                if (!Physics2D.OverlapCircle(transform.position += new Vector3(-player.direction * rawDist, 0, 0), 0.2f, walls)) transform.position += new Vector3(-player.direction * rawDist, 0, 0);
            }
            else if (!player.horizontal)
            {
                if (!Physics2D.OverlapCircle(transform.position += new Vector3(0, -player.direction * rawDist, 0), 0.2f, walls)) transform.position += new Vector3(0f, -player.direction * rawDist, 0f);
            }
        }
    }
}
