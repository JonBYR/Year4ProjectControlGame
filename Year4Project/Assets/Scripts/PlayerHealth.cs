using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    static UnityEngine.Rendering.Universal.FilmGrain grain;
    [SerializeField]
    static int health = 1000;
    public static int originalHealth = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Current Health: " + health;
    }
    public static void TakeDamage()
    {
        health--;
        if (health <= 0 )
        {
            SceneManager.LoadScene("GameOver");
            GameManager.Instance.StopTimer();
        }
    }
    public static void setHealth(int h)
    {
        health = h;
        originalHealth = h;
    }
    public static int getHealth()
    {
        return health;
    }
}
