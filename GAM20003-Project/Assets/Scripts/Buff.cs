using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {
    protected string[] buffString = { "" };
    protected PlayerStats GetStats() {
        return this.transform.parent.parent.gameObject.GetComponent<PlayerStats>(); ;
    }
    // Start is called before the first frame update
    public virtual void ApplyBuff() { }

    protected void SetBuffString(string[] value) { buffString = value; }

    public string[] GetBuffString() { return buffString; }
}
