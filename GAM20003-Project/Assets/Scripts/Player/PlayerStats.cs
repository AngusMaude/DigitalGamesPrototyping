using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockback;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float reloadTime;
    [SerializeField] private float dashTime;
    [SerializeField] private int dashCount;
    [SerializeField] private float bloom;
    private float health { get; set; }

    public float GetMaxHealth() { return maxHealth; }
    public float GetKnockback() { return knockback; }
    public float GetMaxSpeed() { return maxSpeed; }
    public float GetAcceleration() { return acceleration; }
    public float GetJumpHeight() { return jumpHeight; }
    public float GetReloadTime() { return reloadTime; }
    public float GetDashTime() { return dashTime; }
    public int GetDashCount() { return dashCount; }
    public float GetBloom() { return bloom; }


    public void SetMaxHealth(float amount) { maxHealth *= amount; }
    public void SetKnockback(float amount) { knockback *= amount; }
    public void SetMaxSpeed(float amount) { maxSpeed *= amount; }
    public void SetAcceleration(float amount) { maxSpeed *= amount; }
    public void SetJumpHeight(float amount) { jumpHeight *= amount; }
    public void SetReloadTime(float amount) { reloadTime *= amount; }
    public void SetDashTime(float amount) { dashTime *= amount; }
    public void SetDashCount(int amount) { dashCount += amount; }
    public void SetBloom(float amount) { bloom *= amount; }
}
