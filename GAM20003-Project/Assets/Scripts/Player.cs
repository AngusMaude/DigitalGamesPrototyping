using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private PlayerStats stats;
    private BuffHandler buffHandler;
    private string controlScheme;

    private float health;
    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        stats = GetComponent<PlayerStats>();
        buffHandler = transform.Find("BuffHandler").GetComponent<BuffHandler>();
        controlScheme = GetComponent<PlayerInput>().currentControlScheme;

        buffHandler.ApplyAllBuffs();
        health = stats.GetMaxHealth();
        Debug.Log(health);
    }

    private void Update() {
        
    }

    public void Hit(float damage) {
        health -= damage;
        Debug.Log("hit " + health);
        if (health <= 0) {
            Debug.Log("dead");
        }
    }

    public Rigidbody2D GetRigidbody() { return rb; }
    public BoxCollider2D GetCollider() { return coll; }
    public PlayerStats GetStats () { return stats; }
    public string GetControlScheme() { return controlScheme; }

}
