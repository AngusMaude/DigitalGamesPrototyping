using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : Buff
{
    public void Start() {
        title = "Armour Up";
        description[0] = "Increase health by 50%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetMaxHealth(1.5f);
    }
}
