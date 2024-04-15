using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public GameObject oob;
    public GameObject zone;
    private GameManager man;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI infoText;
    public float speed = 5f;
    public float rawDist = 0.1f;
    public static int score = 0;
    public LayerMask walls;
    public bool horizontal;
    public static bool moving;
    public float direction; //these three variables are required for the mimic movement
    public MissedBeat mbeat;
    public WeaponController weapon;
    public static bool hitBeat = false;
    bool alreadyPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        man = GameManager.Instance;
        scoreText.text = "Score: " + score;
        horizontal = false;
        moving = false;
        man.RegisterEnemy(GetInstanceID()); //even though this is on the player, this allows the game to restrict player movement
        man.playerID = GetInstanceID();
        DisplayInfo();
    }
    void DisplayInfo()
    {
        infoText.enabled = true;
        if (WeaponController.currentWeapon == "Baton") infoText.text = "Default weapon. Can only attack in front of you";
        else if (WeaponController.currentWeapon == "Guitar") infoText.text = "Wider width of attack!";
        else if (WeaponController.currentWeapon == "Harp") infoText.text = "Wider height of attack";
        Invoke("RemoveText", 3f);
    }
    void RemoveText()
    {
        infoText.enabled = false;
    }
    void CheckString(string direction)
    {
        if(direction == "Up")
        {
            weapon.ChangeRotation(Quaternion.Euler(0, 0, 90));
        }
        else if(direction == "Down")
        {
            weapon.ChangeRotation(Quaternion.Euler(0, 0, 270));
        }
        else if(direction == "Right")
        {
            weapon.ChangeRotation(Quaternion.Euler(0, 0, 0));
        }
        else if(direction == "Left")
        {
            weapon.ChangeRotation(Quaternion.Euler(0, 0, 180));
        }
        if (WeaponController.currentWeapon == "Guitar")
        {
            if (direction == "Up" || direction == "Down") weapon.ChangeSize(3, 1);
            else if (direction == "Left" || direction == "Right") weapon.ChangeSize(1, 3);
        }
        else if (WeaponController.currentWeapon == "Harp")
        {
            if (direction == "Up") { weapon.ChangeSize(1, 4); weapon.ChangeOffset(0, 2.5f); }
            else if (direction == "Left") { weapon.ChangeSize(4, 1); weapon.ChangeOffset(-2.5f, 0); }
            else if (direction == "Right") { weapon.ChangeSize(4, 1); weapon.ChangeOffset(2.5f, 0); }
            else if(direction == "Down") { weapon.ChangeSize(1, 4); weapon.ChangeOffset(0, -2.5f); }
        }
        else if(WeaponController.currentWeapon == "Baton")
        {
            weapon.ChangeSize(1.5f, 1.5f);
        }
        else if(WeaponController.currentWeapon == "Null")
        {
            weapon.ChangeSize(0, 0);
            weapon.ChangeOffset(0, 0);
        }
        else return;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            man.counter = 0;
            if (man.onBeat == true && !alreadyPressed) //code for movement taken from this tutorial: https://www.youtube.com/watch?v=mbzXIOKZurA
            {
                hitBeat = true;
                score++;
                scoreText.text = "Score: " + score;
                if (Input.GetAxisRaw("Horizontal") == 1f) { weapon.ChangeOffset(1, 0); CheckString("Right"); }
                else if (Input.GetAxisRaw("Horizontal") == -1f) { weapon.ChangeOffset(-1, 0); CheckString("Left"); }
                if (Input.GetAxisRaw("Vertical") == 1f) { weapon.ChangeOffset(0, 1); CheckString("Up"); }
                else if (Input.GetAxisRaw("Vertical") == -1f) { weapon.ChangeOffset(0, -1); CheckString("Down"); }
                if (WeaponController.enemyEntered == true)
                {
                    moving = false;
                }
                else
                {
                    moving = true;
                    if (Input.GetAxisRaw("Horizontal") == 1f || Input.GetAxisRaw("Horizontal") == -1f)
                    {
                        
                        direction = Input.GetAxisRaw("Horizontal");
                        horizontal = true;
                        if (!Physics2D.OverlapCircle(transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * rawDist, 0, 0), 0.2f, walls)) transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * rawDist, 0, 0); //moves position of player to be one tile left or right of player
                                                                                                                                                                                                                                         //this code will check to make sure that if, when moving, the player hits a collider, the player no longer moves
                    }
                    else if (Input.GetAxisRaw("Vertical") == 1f || Input.GetAxisRaw("Vertical") == -1f)
                    {
                        
                        direction = Input.GetAxisRaw("Vertical");
                        horizontal = false;
                        if (!Physics2D.OverlapCircle(transform.position += new Vector3(0, Input.GetAxisRaw("Vertical") * rawDist, 0), 0.2f, walls)) transform.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * rawDist, 0f);
                    }
                }
                //moving = false;
            }
            else
            {
                hitBeat = false;
                moving = false;
                if (score > 10) score = score - 10;
                else if (score > 0) score = score - 1;
                scoreText.text = "Score: " + score;
                StartCoroutine(mbeat.Shake(.15f, .4f));
            }
            alreadyPressed = true;
        }
        if (man.onBeat == false) { moving = false; hitBeat = false; alreadyPressed = false; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && this.gameObject.name == "Player")
        {
            Debug.Log("Collected by" + this.gameObject.name);
            if (collision.name.Contains("Baton")) WeaponController.currentWeapon = "Baton";
            else if (collision.name.Contains("Guitar")) WeaponController.currentWeapon = "Guitar";
            else if (collision.name.Contains("Harp")) WeaponController.currentWeapon = "Harp";
            DisplayInfo();
            Debug.Log(WeaponController.currentWeapon);
            Destroy(collision.gameObject);
            WeaponController.durability = 5;
            WeaponSpawner.spawnWeapon = false;
        }
    }
    public void DoubleScore()
    {
        score = score * 2;
        scoreText.text = "Score: " + score;
    }
    public void OnDestroy()
    {
        man.UnregisterEnemy(GetInstanceID());
    }
}
