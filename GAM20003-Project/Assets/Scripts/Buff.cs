using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    protected PlayerStats GetStats() {
        return this.transform.parent.parent.gameObject.GetComponent<PlayerStats>(); ;
    }
    // Start is called before the first frame update
    public virtual void ApplyBuff() { }
}
