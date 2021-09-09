using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockback;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float reloadTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private int dashCount;
    [SerializeField] private float bloom;
    private float health { get; set; }

    public float GetMaxHealth() { return maxHealth; }
    public float GetKnockback() { return knockback; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetJumpHeight() { return jumpHeight; }
    public float GetReloadTime() { return reloadTime; }
    public float GetDashSpeed() { return dashSpeed; }
    public int GetDashCount() { return dashCount; }
    public float GetBloom() { return bloom; }


    public void SetMaxHealth(float amount) { maxHealth *= amount; }
    public void SetKnockback(float amount) { knockback *= amount; }
    public void SetMoveSpeed(float amount) { moveSpeed *= amount; }
    public void SetJumpHeight(float amount) { jumpHeight *= amount; }
    public void SetReloadTime(float amount) { reloadTime *= amount; }
    public void SetDashSpeed(float amount) { dashSpeed *= amount; }
    public void SetDashCount(int amount) { dashCount += amount; }
    public void SetBloom(float amount) { bloom *= amount; }
}
