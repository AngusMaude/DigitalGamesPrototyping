using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomBuff : Buff
{
    public void Start() {
        title = "Steady Hands";
        description[0] = "Decrease bullet spread by 30%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetBloom(0.7f);
    }
}
