using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sniper : Weapon
{
    private bool charging;
    [SerializeField] private float chargeRate;
    public AudioClip[] ChargeSounds;


    protected override void UpdateBloom() {
        if (charging && weaponCooldown <=0) {

            currentBloom = currentBloom - (Time.deltaTime * chargeRate);
            if (currentBloom <= 0) {
                shooting = true;
                charging = false;
            }
        }
        else {
            currentBloom = bloomMaximum;
        }
    }

    public override void OnShoot(InputValue value) {
        if (value.isPressed) {
            if (weaponCooldown <= 0) {
                charging = true;
                if (ChargeSounds.Length > 0) {
                    WeaponAudio.clip = ChargeSounds[Random.Range(0, ChargeSounds.Length)];
                    WeaponAudio.Play(0);
                }
            }

        }
        else if(charging){
            shooting = true;
            charging = false;
        }
    }
}
