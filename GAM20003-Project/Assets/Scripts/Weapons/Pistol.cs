using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void Reload() {
        Debug.Log("reload");
        reloading = false;
        magAmmo = magSize;
    }
}
