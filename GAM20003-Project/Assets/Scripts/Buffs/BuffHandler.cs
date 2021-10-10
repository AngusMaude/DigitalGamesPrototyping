using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public void ApplyAllBuffs() {
        foreach (Transform buff in transform) {
            buff.GetComponent<Buff>().ApplyBuff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
