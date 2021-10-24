using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : Buff {
    public void Start() {
        title = "Sting Like A Bee";
        description[0] = "Increase damage by 15%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetDamageMultiplier(1.15f);
    }
}
