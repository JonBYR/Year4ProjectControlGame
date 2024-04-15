using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSetting : MonoBehaviour
{
    private GameManager man;
    // Start is called before the first frame update
    void Start()
    {
        man = GameManager.Instance;
        man.setSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
