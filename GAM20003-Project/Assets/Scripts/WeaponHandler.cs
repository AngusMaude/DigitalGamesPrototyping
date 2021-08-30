using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour {
    private GameObject sceneWeapons;

    [SerializeField] private float weaponReach;
    [SerializeField] private float dropRotation;
    [SerializeField] private float dropForce;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneWeapons = GameObject.Find("SceneWeapons");
    }

    void OnPickUp() {
        foreach (Transform weapon in sceneWeapons.transform) {
            Debug.Log(Vector3.Distance(transform.position, weapon.position));
            if (Vector3.Distance(transform.position, weapon.position) < weaponReach) {
                Debug.Log("weapon found");
                Drop();
                PickUp(weapon);
                weapon.GetComponent<Weapon>().enabled = true;
                break;
            }
        }
    }

    void PickUp(Transform weapon) {
        weapon.SetParent(transform);
        weapon.GetComponent<Weapon>().enabled = false;
        weapon.GetComponent<Rigidbody2D>().simulated = false;
        weapon.position = transform.position;
    }

    void OnDrop() {
        Drop();
    }

    void Drop() {
        foreach (Transform weapon in transform) {
            Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
            weapon.GetComponent<Weapon>().enabled = false;
            rb.simulated = true;
            rb.velocity = new Vector2((Random.value - 0.5f) * dropForce, dropForce);
            rb.AddTorque((Random.value - 0.5f) * dropRotation);

            weapon.SetParent(sceneWeapons.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
