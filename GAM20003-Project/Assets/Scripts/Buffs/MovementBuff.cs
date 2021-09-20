using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuff : Buff
{
    public void Start() {
        string[] temp = {
            "Increase Move Speed by 100%"
        };
        SetBuffString(temp);
    }
    public override void ApplyBuff() {
        PlayerStats stats = GetStats();
        stats.SetMaxSpeed(2f);
    }
}
