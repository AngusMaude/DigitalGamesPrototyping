using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private bool randomWeapons;
    [SerializeField] private GameObject spawnWeapon;
    [SerializeField] private GameObject[] weaponList;
    [SerializeField] private float spawnTime;
    private float timer;

    void Start()
    {
        timer = spawnTime;
        Spawn();
    }

    private void Spawn() {
        GameObject spawn = Instantiate(GetWeapon(), transform);
    }

    private GameObject GetWeapon() {
        if (randomWeapons)
            return weaponList[Random.Range(0, weaponList.Length)];
        else
            return spawnWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) {
            if (timer <= 0) {
                Spawn();
                timer = spawnTime;
            }

            timer -= Time.deltaTime;
        }
    }
}
