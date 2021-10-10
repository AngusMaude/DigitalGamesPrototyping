using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxPlayers;
    public IDictionary<int, Player> activePlayers = new Dictionary<int, Player>();
    //public List<Player> activePlayers = new List<Player>();
    public IDictionary<int, PlayerSpawner> spawners = new Dictionary<int, PlayerSpawner>();
    public GameObject spawnEffect;
    private Scene currentScene;
    private void Awake() {
        if(instance == null) {

       
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update

    void Start() {
        currentScene = SceneManager.GetActiveScene();
        PlayerSpawner[] spawnpoints = FindObjectsOfType(typeof(PlayerSpawner)) as PlayerSpawner[];
        foreach(PlayerSpawner spawn in spawnpoints) {
            spawners.Add(spawn.spawnID, spawn);
        }
    }

    // Update is called once per frame
    void Update() {
        Scene thisScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex != thisScene.buildIndex) {
            ChangedActiveScene(currentScene, thisScene);
            currentScene = thisScene;
            //transform.position = originalPos;
        }

        foreach (KeyValuePair<int, Player> entry in activePlayers) {
            if (entry.Value.GetHealth() <= 0) {
                entry.Value.gameObject.SetActive(false);
                if (CheckRoundEnd()) {
                    // TEMP FOR PLAYTEST
                    ResetArena();
                }
            }
        }
    }

    private void ResetArena() {
        foreach (KeyValuePair<int, Player> entry in activePlayers) {
            entry.Value.gameObject.SetActive(true);
            entry.Value.ChangeScenes();
            spawners[entry.Value.playerID].SpawnPlayer();
        }
    }

    private bool CheckRoundEnd() {
        int i = 0;
        foreach (KeyValuePair<int, Player> entry in activePlayers) {
            if (entry.Value.gameObject.activeSelf) {
                i++;
            }
        }
        return i <= 1;
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        foreach(KeyValuePair<int, Player> entry in activePlayers) {
            entry.Value.ChangeScenes();
        }

        string currentName = current.name;
        if (currentName == null) {
            spawners.Clear();
            PlayerSpawner[] spawnpoints = FindObjectsOfType(typeof(PlayerSpawner)) as PlayerSpawner[];
            foreach (PlayerSpawner spawn in spawnpoints) {
                Debug.Log("Spawner found with ID: " + spawn.spawnID);
                spawners.Add(spawn.spawnID, spawn);
                if (activePlayers.ContainsKey(spawn.spawnID)) {
                    Debug.Log("Player found with ID: " + spawn.spawnID);
                    activePlayers[spawn.spawnID].AssignSpawn(spawn);
                    spawn.SpawnPlayer();
                }
            }
            Debug.Log("Spawner list length: " + spawners.Count);
            // Scene1 has been removed
            currentName = "Replaced";
            Debug.Log("Scenes: " + currentName + ", " + next.name);
        }
    }

    public void AddPlayer(Player newPlayer) {
        if (activePlayers.Count < maxPlayers) {
            newPlayer.AssignID(activePlayers.Count);
            activePlayers.Add(newPlayer.playerID, newPlayer);
            Debug.Log("Player added with ID: " + newPlayer.playerID);
            Instantiate(spawnEffect, newPlayer.transform.position, newPlayer.transform.rotation);
            newPlayer.transform.SetParent(this.transform);
        }
        else {
            Destroy(newPlayer.gameObject);
        }
    }
}
