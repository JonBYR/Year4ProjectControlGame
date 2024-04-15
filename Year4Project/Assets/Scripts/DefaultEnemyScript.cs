using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyScript : MonoBehaviour
{
    GameManager man;
    bool shrink = false;
    // Start is called before the first frame update
    void Start()
    {
        man = GameManager.Instance;
        man.RegisterEnemy(GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        if (man.onBeat == true && man.returnEnemyMove(GetInstanceID())) //checks that the enemy is within the beat and that the enemy is allowed to move
        {
            man.setEnemyMove(GetInstanceID()); //this will set the enemy to be false after beat is called so the enemy doesn't continually move
            if (shrink == false)
            {
                this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            }
            else if (shrink == true) this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            shrink = !shrink;

        }
    }
}
