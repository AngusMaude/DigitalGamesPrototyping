using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {
    [SerializeField] private int pelletCount;
    protected override void Shoot() {
        for (int i = 0; i < pelletCount; i++) {
            base.Shoot();
        }
    }
}
