using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplayer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Your final score was: " + PlayerController.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
