using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDashBuff : Buff
{
    public void Start() {
        title = "Spare Thruster";
        description[0] = "Gain an extra dash";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetDashCount(1);
    }
}
