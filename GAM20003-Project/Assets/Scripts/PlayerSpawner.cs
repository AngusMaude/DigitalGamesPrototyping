using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour {

    [SerializeField] protected int spawnID = -1;
    [SerializeField] protected float spawnTime;
    [SerializeField] protected Player playerToSpawn;
    private float spawning;
    //private Transform location;
    public Vector2 spawnLocation;

    void SpawnPlayer() {
        spawning = spawnTime;
        //playerToSpawn.GetRigidbody();
    }
    // Start is called before the first frame update
    void Start() {
        spawnLocation = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update() {
        if (spawning > 0) {
            spawning = Mathf.Clamp(spawning - Time.deltaTime, 0, spawnTime);
            playerToSpawn.GetRigidbody().position = Vector2.Lerp(playerToSpawn.GetRigidbody().position, spawnLocation, Time.deltaTime);
        }
    }
}
