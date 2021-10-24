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
    private List<Player> buffOrder = new List<Player>();
    private GameObject sceneWeapons;
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
        sceneWeapons = transform.Find("SceneWeapons").gameObject;
        foreach (PlayerSpawner spawn in spawnpoints) {
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

        switch (thisScene.name) {
            case "Lobby":
                break;
            case "Buffs":
                if (buffOrder.Count == 0) {
                    SceneManager.LoadScene("Map1");
                    SetAllPlayers(true);
                    foreach (KeyValuePair<int, Player> entry in activePlayers) {
                        entry.Value.ResetStats();
                    }
                }
                break;
            default:
                foreach (KeyValuePair<int, Player> entry in activePlayers) {
                    if (entry.Value.GetHealth() <= 0) {
                        buffOrder.Add(entry.Value);
                        entry.Value.gameObject.SetActive(false);
                        if (CheckRoundEnd()) {
                            SetAllPlayers(true);
                            SceneManager.LoadScene("Buffs");
                        }
                    }
                }
                break;
        }
    }

    private void SetAllPlayers(bool value) {
        foreach (KeyValuePair<int, Player> entry in activePlayers) {
            entry.Value.gameObject.SetActive(value);        }
    }

    private void ResetPlayers() {
        foreach (KeyValuePair<int, Player> entry in activePlayers) {
            entry.Value.ResetStats();
            entry.Value.gameObject.SetActive(true);
            entry.Value.ChangeScenes();
            spawners[entry.Value.playerID].SpawnPlayer();
        }
    }

    private bool CheckRoundEnd() {
        if (buffOrder.Count >= activePlayers.Count - 1) {
            return true;
        }
        return false;
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
                spawners.Add(spawn.spawnID, spawn);
                if (activePlayers.ContainsKey(spawn.spawnID)) {
                    activePlayers[spawn.spawnID].AssignSpawn(spawn);
                    spawn.SpawnPlayer();
                }
            }

            ResetSceneWeapons();
        }
    }

    private void ResetSceneWeapons() {
        foreach (Transform child in sceneWeapons.transform) {
            if (child.name != "DroppedWeapons") {
                Destroy(child.gameObject);
            }
            else {
                foreach(Transform weapon in child.gameObject.transform) {
                    Destroy(weapon.gameObject);
                }
            }
        }

        foreach (WeaponSpawner weaponSpawner in FindObjectsOfType(typeof(WeaponSpawner)) as WeaponSpawner[]) {
            weaponSpawner.transform.SetParent(sceneWeapons.transform);
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

    public void SelectedBuff(Buff buff) {
        buffOrder[0].AddBuff(buff);
        buffOrder.RemoveAt(0);
    }
}
