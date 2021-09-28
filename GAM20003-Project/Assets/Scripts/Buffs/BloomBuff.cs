using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomBuff : Buff
{
    public void Start() {
        string[] temp = {
            "Decrease bloom by 30%"
        };
        SetBuffString(temp);
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetBloom(0.7f);
    }
}
