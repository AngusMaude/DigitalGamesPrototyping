using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloomLines : MonoBehaviour {

    [SerializeField] private LineRenderer bloomLine1;
    [SerializeField] private LineRenderer bloomLine2;


    // Start is called before the first frame update
    void Start()
    {
        LineRenderer[] bloomLines = GetComponentsInChildren<LineRenderer>();
        bloomLine1 = bloomLines[0];
        bloomLine2 = bloomLines[1];
    }

    public void EnableLines() {
        Debug.Log("Enabling lines.");
        bloomLine1.enabled = true;
        bloomLine2.enabled = true;
    }

    public void DisableLines() {
        Debug.Log("Disabling lines.");
        bloomLine1.enabled = false;
        bloomLine2.enabled = false;
    }


    public void ShowBloom(Vector3 firePos, Vector3 bloomPos1, Vector3 bloomPos2) {
        bloomLine1.SetPosition(0, firePos);
        bloomLine1.SetPosition(1, firePos + bloomPos1);
        bloomLine2.SetPosition(0, firePos);
        bloomLine2.SetPosition(1, firePos + bloomPos2);
    }
}
