using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuff : Buff
{
    public void Start() {
        title = "Float like a butterfly";
        description[0] = "Increase move speed by 30%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetMaxSpeed(1.3f);
    }
}
