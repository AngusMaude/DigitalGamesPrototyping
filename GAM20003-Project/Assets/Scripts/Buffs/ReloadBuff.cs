using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBuff : Buff
{
    public void Start() {
        string[] temp = {
            "Decrease reload time by 30%"
        };
        SetBuffString(temp);
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetReloadTime(0.7f);
    }
}
