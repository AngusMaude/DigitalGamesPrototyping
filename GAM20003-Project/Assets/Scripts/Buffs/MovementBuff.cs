using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuff : Buff
{
    public void Start() {
        string[] temp = {
            "Increase move speed by 30%"
        };
        SetBuffString(temp);
    }

    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetMaxSpeed(1.3f);
    }
}
