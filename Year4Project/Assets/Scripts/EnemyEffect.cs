using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3); //makes sure that particle effect clone is deleted from scene for optimisation
    }

    // Update is called once per frame
    void Update()
    {
        //https://www.youtube.com/watch?v=PI04VdO-j_0 used to create particle
    }
}
