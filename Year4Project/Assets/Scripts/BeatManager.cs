using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BeatManager : MonoBehaviour
{
    public GameManager man;
    public TextMeshProUGUI scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.anyKeyDown)
        {
            if (man.onBeat == true)
            {
                Debug.Log("BPM matched!"); //singleton used to access global information. If the user is pressing within beat, user can move
                score++;
                scoreText.text = "Score: " + score;
            }
            else Debug.Log("BPM failed!");
        }
    }
}
