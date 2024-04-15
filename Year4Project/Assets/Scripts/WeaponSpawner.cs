using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public static bool spawnWeapon;
    public Transform playerPosition;
    public float spawnRadius;
    public GameObject[] weapons = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        spawnWeapon = false;
    }
    void SpawnWeapon()
    {
        float x = UnityEngine.Random.Range(-6.5f, 6);
        float y = UnityEngine.Random.Range(-3, 3);
        Vector3 spawnPos = new Vector3(x, y, 0);
        if((playerPosition.transform.position - spawnPos).magnitude <= spawnRadius ) 
        {
            return;
        }
        else
        {
            int spawnRandomWeapon = (int) UnityEngine.Random.Range(0, 3);
            if (spawnRandomWeapon == 3) spawnRandomWeapon = 2;
            Instantiate(weapons[spawnRandomWeapon], spawnPos, Quaternion.identity);
            spawnWeapon = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if ((WeaponController.durability <= 0) && spawnWeapon == false)
        {
            SpawnWeapon();
        }
    }
}
