using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDistanceBuff : Buff {
    public void Start() {
        title = "Fly Further";
        description[0] = "Increase dash distance by 20%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetDashTime(1.2f);
    }
}
