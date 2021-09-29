using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void Reload() {
        Debug.Log("reload");
        if (weaponCooldown <= 0)
        {
            reloading = false;
            magAmmo = magSize;
            player.UpdateUIReloadTimer(reloadTime * player.GetStats().GetReloadTime(), 0);
        }
    }
}
