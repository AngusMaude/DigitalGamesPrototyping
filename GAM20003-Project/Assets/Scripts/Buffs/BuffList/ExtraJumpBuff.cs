using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraJumpBuff : Buff {
    public void Start() {
        title = "Double Jump";
        description[0] = "Gain an extra jump";
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetJumpCount(1);
    }
}
