using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWeapons : MonoBehaviour
{
    [SerializeField] private float spawnTimerMax;
    private float spawnTimer;
    [SerializeField] private GameObject[] weapons;

    void Start()
    {
    }

    private void Spawn() {
        float distanceFromCamera = Camera.main.nearClipPlane; // Change this value if you want
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanceFromCamera));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceFromCamera));
        Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, Random.value); // Get a random point between the topLeft and topRight point

        int randomIndex = Random.Range(0, weapons.Length);
        GameObject spawn = Instantiate(weapons[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        spawn.transform.SetParent(transform);
        Debug.DrawRay(Vector3.zero, Camera.main.ScreenToViewportPoint(Input.mousePosition), Color.red, 10f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0) {
            Spawn();
            spawnTimer = spawnTimerMax;
        }

        spawnTimer -= Time.deltaTime;
    }
}
