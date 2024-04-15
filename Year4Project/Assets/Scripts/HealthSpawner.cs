using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    bool spawnHealth;
    public Transform playerPosition;
    public float spawnRadius;
    public GameObject health;
    // Start is called before the first frame update
    void SpawnHealth()
    {
        float x = UnityEngine.Random.Range(-6.5f, 6);
        float y = UnityEngine.Random.Range(-3, 3);
        Vector3 spawnPos = new Vector3(x, y, 0);
        if ((playerPosition.transform.position - spawnPos).magnitude <= spawnRadius)
        {
            return;
        }
        else
        {
            Instantiate(health, spawnPos, Quaternion.identity);
            spawnHealth = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.getHealth() <= (PlayerHealth.originalHealth / 2) && spawnHealth == false)
        {
            SpawnHealth();
        }
    }
}
