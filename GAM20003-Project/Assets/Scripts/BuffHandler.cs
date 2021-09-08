using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ApplyAllBuffs();
    }

    private void ApplyAllBuffs() {
        foreach (Transform buff in transform) {
            buff.GetComponent<Buff>().ApplyBuff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
