using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //taken from this githib repo https://github.com/JJMaslen/WalkWithRhythm
    public AudioSource music;
    public AudioClip clip;
    public static GameManager Instance { get; private set; }
    public float songBpm; //bpm of song (this will be changed to be equal to values from a json file)
    //public int songKey;
    public float duration;
    private float durationTimer;
    public int margin;
    public int timer;
    public double dTimer;
    public double dMargin;
    public bool onBeat = false; //used to determine if a player can move as it is within the bpm
    public Dictionary<int, bool> enemyObjects = new Dictionary<int, bool>();
    public int playerID;
    bool beatDone = false;
    private Slider s;
    private Color red = new Color(255f, 0f, 0f);
    private Color green = new Color(0f, 255f, 0f);
    private int beatCount;
    public bool enemySmall = false;
    public GameObject defaultObject;
    public int counter;
    public double bpmConversion(double bpm)
    {
        double fixedUpdateBpm = 60 / bpm; //fixed Update is 50 frames rather than 60 frames per second, so bpm must be converted to match the timing for fixedUpdate
        s.maxValue = (float)fixedUpdateBpm;
        return fixedUpdateBpm;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this) //how to instantiate instances in unity
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void setMargin(float t)
    {
        if (t < 100)
        {
            dMargin = 0.275; //slower songs need more margin of error
        }
        else
        {
            dMargin = 0.15;
        }
    }
    // Start is called before the first frame update
    public void RegisterEnemy(int enemyID)
    {
        enemyObjects.Add(enemyID, true); //add new value in dictionary with unique enemy id and set to true
    }
    public void UnregisterEnemy(int enemyID)
    {
        enemyObjects.Remove(enemyID);
    }
    public bool GetEnemySize()
    {
        Debug.Log("Size" + enemySmall);
        return enemySmall;
    }
    public void setTimer()
    {
        durationTimer = 0;
        StartCoroutine("StartTimer");
    }
    public void StopTimer()
    {
        StopCoroutine("StartTimer");
    }
    public void setEnemyMove(int enemyID)
    {
        enemyObjects[enemyID] = false;
    }
    public bool returnEnemyMove(int enemyID)
    {
        if (beatCount % 2 == 0) return enemyObjects[enemyID];
        else return false;
    }
    public void setSlider()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            s = GameObject.Find("Slider").GetComponent<Slider>();
        }
    }
    public void startMusic()
    {
        Debug.Log("Call music function");
        music.Play();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            defaultObject = GameObject.Find("Default");
        }
        else
        {
            defaultObject = null;
        }
    }
    void Update() 
    {

        if (SceneManager.GetActiveScene().name != "GameScene") return;
        else
        {
            if (durationTimer >= duration)
            {
                SceneManager.LoadScene("WinScene");
                StopCoroutine(StartTimer());
            }
        }
        dTimer += Time.deltaTime;
        s.value = (float)dTimer;
        double bpm = bpmConversion(songBpm);
        if(dTimer >= bpm - dMargin || dTimer < 0.0+dMargin) 
        {
            onBeat = true;
            s.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = green;
            if (beatDone == false) 
            {
                
                beatDone = true;
                counter++;
                if (counter >= 10) SceneManager.LoadScene("GameOver");
            }
            
        }
        else 
        {
            if(beatDone == true)
            {
                beatDone = false;
                beatCount++;
                foreach (int id in enemyObjects.Keys.ToList()) //once beat is completed let enemies move again
                {
                    enemyObjects[id] = true;
                }
            }
            s.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
            onBeat = false;
        }
        if (dTimer >= bpm) { dTimer = 0; s.value = 0f; }
    }
    IEnumerator StartTimer()
    {
        while(durationTimer <= duration)
        {
            durationTimer++;
            yield return new WaitForSeconds(1.0f);
        }
    }
    /*
    void FixedUpdate()
    {
        timer++;
        int beat = (int)(bpmConversion(songBpm) * 50); //converts beat to bpm
        int count = timer % beat; //check current time between beats
        if (count >= beat - margin) //if between beats (or slightly before)
        {
            if (onBeat == false)
            {
                onBeat = true;
                if(beatDone == false) beatDone = true;
            }
            onBeat = true;
        }
        else if (count <= margin) //accounts for lateness
        {
            onBeat = true;
        }
        else
        {
            if(beatDone == true)
            {
                beatDone = false;
                foreach(int id in enemyObjects.Keys.ToList()) //once beat is completed let enemies move again
                {
                    enemyObjects[id] = true;
                }
            }
            onBeat = false;
        }
    }
    */
}
