using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private PlayerStats stats;
    private BuffHandler buffHandler;
    private WeaponHandler weaponHandler;
    private string controlScheme;
    private SpriteRenderer spriteR;
    [SerializeField] private Sprite[] playerSpriteList;

    [SerializeField] public int playerID = -1;
    private PlayerSpawner spawner;
    [SerializeField] private Vector2 spawnPoint;

    public ProgressBar healthBar;
    public ProgressBar ammoCount;
    public ProgressBar reloadTimer;

    private float health;
    private float maxHealth;

    public void ChangeScenes() {
        weaponHandler.ChangeScene();
        health = stats.GetMaxHealth();
        healthBar.UpdateProgressBar(maxHealth, health);
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        
        buffHandler = transform.Find("BuffHandler").GetComponent<BuffHandler>();
        weaponHandler = transform.Find("WeaponHandler").GetComponent<WeaponHandler>();
        controlScheme = GetComponent<PlayerInput>().currentControlScheme;
        spriteR = GetComponent<SpriteRenderer>();

        stats = GetComponent<PlayerStats>();
        stats.ResetStats();
        health = stats.GetMaxHealth();
        maxHealth = health;
        healthBar.UpdateProgressBar(maxHealth, health);

        UpdateUIReloadTimer(1f, 0f);
    }

    private void Update() {
        Debug.Log(health);
    }

    public void AssignID(int newID) {
        playerID = newID;
        spriteR.sprite = playerSpriteList[playerID];
    }

    public void AssignSpawn(PlayerSpawner spawn) {
        spawner = spawn;
        spawner.playerToSpawn = this;
    }

    public void Hit(float damage) {
        health -= damage;
        Debug.Log("hit " + health);
        healthBar.UpdateProgressBar(maxHealth, health);
    }

    public void UpdateUIAmmoCount(int maxAmmo, int remainingAmmo){
        ammoCount.UpdateProgressBar(maxAmmo, remainingAmmo);
    }

    public void UpdateUIReloadTimer(float maxReloadTime, float remainingTime) {
        reloadTimer.UpdateProgressBar(maxReloadTime, remainingTime);

    }

    public void AddBuff(Buff buff) {
        buff.transform.SetParent(buffHandler.transform);
    }
    public void ResetStats() {
        stats.ResetStats();
        buffHandler.ApplyAllBuffs();
        health = stats.GetMaxHealth();
    }

    public Rigidbody2D GetRigidbody() { return rb; }
    public BoxCollider2D GetCollider() { return coll; }
    public PlayerStats GetStats () { return stats; }
    public string GetControlScheme() { return controlScheme; }
    public float GetHealth() { return health; }

    

}
