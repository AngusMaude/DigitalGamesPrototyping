using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {
    protected string title = "";
    protected string[] description = { "" };
    protected PlayerStats GetStats() {
        return this.transform.parent.parent.gameObject.GetComponent<PlayerStats>(); ;
    }

    public virtual void ApplyBuff() { }

    public string[] GetDescription() { return description; }

    public string GetTitle() { return title; }
}
