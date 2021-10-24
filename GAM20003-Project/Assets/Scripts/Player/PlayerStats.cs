using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float defMaxHealth;
    [SerializeField] private float defKnockback;
    [SerializeField] private float defMaxSpeed;
    [SerializeField] private float defAcceleration;
    [SerializeField] private float defJumpHeight;
    [SerializeField] private float defReloadTime;
    [SerializeField] private float defDamageMultiplier;
    [SerializeField] private float defDashTime;
    [SerializeField] private int defDashCount;
    [SerializeField] private int defJumpCount;
    [SerializeField] private float defBloom;
    [SerializeField] private float defWallFriction;

    private float maxHealth;
    private float knockback;
    private float maxSpeed;
    private float acceleration;
    private float jumpHeight;
    private float reloadTime;
    private float damageMultiplier;
    private float dashTime;
    private int dashCount;
    private int jumpCount;
    private float bloom;
    private float wallFriction;
    
    public void ResetStats() {
        maxHealth = defMaxHealth;
        knockback = defKnockback;
        maxSpeed = defMaxSpeed;
        acceleration = defAcceleration;
        jumpHeight = defJumpHeight;
        reloadTime = defReloadTime;
        damageMultiplier = defDamageMultiplier;
        dashTime = defDashTime;
        dashCount = defDashCount;
        jumpCount = defJumpCount;
        bloom = defBloom;
        wallFriction = defWallFriction;
    }

    public float GetMaxHealth() { return maxHealth; }
    public float GetKnockback() { return knockback; }
    public float GetMaxSpeed() { return maxSpeed; }
    public float GetAcceleration() { return acceleration; }
    public float GetJumpHeight() { return jumpHeight; }
    public float GetReloadTime() { return reloadTime; }
    public float GetDamageMultiplier() { return damageMultiplier; }
    public float GetDashTime() { return dashTime; }
    public int GetDashCount() { return dashCount; }
    public int GetJumpCount() { return jumpCount; }
    public float GetBloom() { return bloom; }
    public float GetWallFriction() { return wallFriction; }

    public void SetMaxHealth(float amount) { maxHealth *= amount; }
    public void SetKnockback(float amount) { knockback *= amount; }
    public void SetMaxSpeed(float amount) { maxSpeed *= amount; }
    public void SetAcceleration(float amount) { maxSpeed *= amount; }
    public void SetJumpHeight(float amount) { jumpHeight *= amount; }
    public void SetReloadTime(float amount) { reloadTime *= amount; }
    public void SetDamageMultiplier(float amount) { damageMultiplier *= amount; }
    public void SetDashTime(float amount) { dashTime *= amount; }
    public void SetDashCount(int amount) { dashCount += amount; }
    public void SetJumpCount(int amount) { jumpCount += amount; }
    public void SetBloom(float amount) { bloom *= amount; }
    public void SetWallFriction(float amount) { wallFriction *= amount; }
}
