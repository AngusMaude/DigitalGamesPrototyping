using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float knockback = 1f;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 20.0f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private float dashSpeed = 3f;
    [SerializeField] private int dashCount = 1;
    [SerializeField] private float bloom = 1f;

    public float GetHealth() { return health; }
    public float GetKnockback() { return knockback; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetJumpHeight() { return jumpHeight; }
    public float GetReloadTime() { return reloadTime; }
    public float GetDashSpeed() { return dashSpeed; }
    public int GetDashCount() { return dashCount; }
    public float GetBloom() { return bloom; }


    public void SetHealth(float amount) { health *= amount; }
    public void SetKnockback(float amount) { knockback *= amount; }
    public void SetMoveSpeed(float amount) { moveSpeed *= amount; }
    public void SetJumpHeight(float amount) { jumpHeight *= amount; }
    public void SetReloadTime(float amount) { reloadTime *= amount; }
    public void SetDashSpeed(float amount) { dashSpeed *= amount; }
    public void SetDashCount(int amount) { dashCount += amount; }
    public void SetBloom(float amount) { bloom *= amount; }
}
