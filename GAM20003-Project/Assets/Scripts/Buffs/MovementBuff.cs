using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuff : Buff
{
    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetMoveSpeed(2f);
    }
}