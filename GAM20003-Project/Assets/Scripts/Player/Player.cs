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
    private PlayerSpawner spawner;
    [SerializeField] public int playerID;
    [SerializeField] private Vector2 spawnPoint;

    public ProgressBar healthBar;
    public ProgressBar ammoCount;
    public ProgressBar reloadTimer;

    private float health;
    private float maxHealth;
    
    
    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        stats = GetComponent<PlayerStats>();
        buffHandler = transform.Find("BuffHandler").GetComponent<BuffHandler>();
        weaponHandler = transform.Find("WeaponHandler").GetComponent<WeaponHandler>();
        controlScheme = GetComponent<PlayerInput>().currentControlScheme;
        
        buffHandler.ApplyAllBuffs();
        weaponHandler.ChangeScene();
        health = stats.GetMaxHealth();
        maxHealth = health;
        healthBar.UpdateProgressBar(maxHealth, health);

        Debug.Log(health);

        UpdateUIReloadTimer(1f, 0f);
    }

    private void Update() {

    }

    public void AssignSpawn(PlayerSpawner spawn) {
        spawner = spawn;
        spawner.playerToSpawn = this;
    }

    public void Hit(float damage) {
        health -= damage;
        Debug.Log("hit " + health);
        if (health <= 0) {
            Debug.Log("dead");
            SceneManager.LoadScene("Buffs");
            health = stats.GetMaxHealth();
            rb.position = spawnPoint;
        }
        healthBar.UpdateProgressBar(maxHealth, health);
    }

    public void UpdateUIAmmoCount(int maxAmmo, int remainingAmmo){
        ammoCount.UpdateProgressBar(maxAmmo, remainingAmmo);
    }

    public void UpdateUIReloadTimer(float maxReloadTime, float remainingTime) {
        reloadTimer.UpdateProgressBar(maxReloadTime, remainingTime);

    }

    public void ChangeScenes() {
        weaponHandler.ChangeScene();
    }

    public Rigidbody2D GetRigidbody() { return rb; }
    public BoxCollider2D GetCollider() { return coll; }
    public PlayerStats GetStats () { return stats; }
    public string GetControlScheme() { return controlScheme; }

}
