using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDashBuff : Buff
{
    public void Start() {
        string[] temp = {
            "Gain an extra dash"
        };
        SetBuffString(temp);
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetDashCount(1);
    }
}
