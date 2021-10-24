using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackBuff : Buff {
    public void Start() {
        title = "Bullet Punch";
        description[0] = "Increase knockback by 30%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetKnockback(1.3f);
    }
}
