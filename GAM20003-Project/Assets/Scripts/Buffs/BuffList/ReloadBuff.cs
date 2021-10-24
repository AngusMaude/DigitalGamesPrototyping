using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBuff : Buff
{
    public void Start() {
        title = "Fast Hands";
        description[0] = "Decrease reload time by 30%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetReloadTime(0.7f);
    }
}
