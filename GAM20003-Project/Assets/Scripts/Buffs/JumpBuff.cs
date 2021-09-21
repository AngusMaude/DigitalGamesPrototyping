using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuff : Buff
{
    public void Start() {
        string[] temp = {
            "Increase jump by 30%"
        };
        SetBuffString(temp);
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetJumpHeight(1.3f);
    }
}
