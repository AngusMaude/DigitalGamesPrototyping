using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuff : Buff
{
    public void Start() {
        title = "Leg Day";
        description[0] = "Increase jump by 30%";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetJumpHeight(1.3f);
    }
}
