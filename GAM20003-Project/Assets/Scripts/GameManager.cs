using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
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

    public AudioSource musicAudioSource;
    private AudioLowPassFilter lowpass;
    private int musicPlayItt = 0;
    public AudioClip menuMusic;
    public AudioClip[] map1Music;
    public AudioClip buffMusic;
    public AudioClip lobbyMusic;
    private double musicStartTime;
    public float musicVolume = 0.5f;

    private void Awake() {
        if(instance == null) {

       
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void PlayAudio(Scene thisScene, bool newScene) {
        if (newScene) {
            switch (thisScene.name) {
                case "Lobby":
                    musicAudioSource.clip = lobbyMusic;
                    break;
                case "Buffs":
                    //musicAudioSource.clip = buffMusic;
                    //audioMixer.TransitionToSnapshots(snapshotArray, weightsArray, time.DeltaTime);
                    break;
                case "Menu":
                    musicAudioSource.clip = menuMusic;
                    break;
                default:
                    if (musicPlayItt == 0) {
                        musicAudioSource.clip = map1Music[0];
                        musicPlayItt = 1;
                    } else {
                        musicAudioSource.clip = map1Music[1];
                        musicPlayItt = 0;
                    }
                    break;
            }
            if (thisScene.name != "Buffs") {
                double startTime = AudioSettings.dspTime + 0.2;
                musicAudioSource.PlayScheduled(startTime);
                lowpass.cutoffFrequency = 22000;
            } else {
                lowpass.cutoffFrequency = 4000;
            }
        }

    }
    // Start is called before the first frame update

    void Start() {
        musicAudioSource = GetComponent<AudioSource>();
        lowpass = GetComponent<AudioLowPassFilter>();
        musicAudioSource.volume = musicVolume;

        currentScene = SceneManager.GetActiveScene();
        PlayerSpawner[] spawnpoints = FindObjectsOfType(typeof(PlayerSpawner)) as PlayerSpawner[];
        sceneWeapons = transform.Find("SceneWeapons").gameObject;
        PlayAudio(currentScene, true);

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
            PlayAudio(currentScene, true);
            //transform.position = originalPos;
        } //else {
           // SetAudio(currentScene, false);
        //}
        //double time = AudioSettings.dspTime;
        //if (time + 2f > musicStartTime) {
        //    musicSources[flip].PlayScheduled(musicStartTime);
        //    double clipDuration = (double)clips[flip].samples / (double)clips[flip].frequency;
        //    
        //    musicStartTime += clipDuration;
        //    flip = 1 - flip;
        //    Debug.Log("Time is " + time + ". Source " + flip + " scheduled queue to play in " + (musicStartTime - time) + ", at " + musicStartTime);
        //
        //
        //    // Flip between two audio sources so that the loading process of one does not interfere with the one that's playing out

        //}

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
                            int i = -15;
                            foreach (KeyValuePair<int, Player> p in activePlayers) {
                                p.Value.GetRigidbody().position = new Vector2(i, -1);
                                i += 10;
                            }

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
            newPlayer.ActivateInput();
        }
        else {
            Destroy(newPlayer.gameObject);
        }
    }

    public bool SelectedBuff(Buff buff, Player player) {
        if (player == buffOrder[0]) {
            buffOrder[0].AddBuff(buff);
            buffOrder.RemoveAt(0);
            return true;
        }
        else
            return false;
    }

    public Player GetBuffPlayer() { return buffOrder[0]; }
}
