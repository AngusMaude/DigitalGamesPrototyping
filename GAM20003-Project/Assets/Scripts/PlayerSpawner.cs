using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour {

    public int spawnID = -1;
    [SerializeField] protected float spawnTime;
    public Player playerToSpawn;
    private float spawning;
    //private Transform location;
    private Vector2 spawnLocation;

    public void SpawnPlayer() {
        //spawning = spawnTime;
        playerToSpawn.GetRigidbody().position = spawnLocation;
        //playerToSpawn.GetRigidbody();
    }
    // Start is called before the first frame update
    void Start() {
        spawnLocation = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update() {
        // Fancy spawning for later
        //if (spawning > 0) {
        //    spawning = Mathf.Clamp(spawning - Time.deltaTime, 0, spawnTime);
        //    playerToSpawn.GetRigidbody().position = spawnLocation;
        //}
    }
}
