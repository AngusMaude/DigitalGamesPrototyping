using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour {
    private GameObject sceneWeapons;

    [SerializeField] private float weaponReach;
    [SerializeField] private float dropRotation;
    [SerializeField] private float dropForce;
    [SerializeField] private GameObject defaultWeapon;
    private Transform droppedWeapons;
    private string defaultName;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneWeapons = GameObject.Find("SceneWeapons");
        droppedWeapons = sceneWeapons.transform.Find("DroppedWeapons");
        Instantiate(defaultWeapon, transform);
        defaultName = defaultWeapon.name;
    }

    void OnPickUp() {
        foreach (Transform container in sceneWeapons.transform) {
            foreach (Transform weapon in container) {
                if (Vector3.Distance(transform.position, weapon.position) < weaponReach) {
                    Drop();
                    PickUp(weapon);
                    weapon.GetComponent<Weapon>().enabled = true;
                    break;
                }
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
        if (!transform.GetChild(0).name.Contains(defaultName)) {
            Drop();
            GameObject weapon = Instantiate(defaultWeapon, transform) as GameObject;
            weapon.GetComponent<Weapon>().SetInfiniteAmmo(true);
        }
    }

    void Drop() {
        foreach (Transform weapon in transform) {
            if (weapon.name.Contains(defaultName)) {
                Destroy(weapon.gameObject);
            }
            else {
                Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
                weapon.GetComponent<Weapon>().enabled = false;
                rb.simulated = true;
                rb.velocity = new Vector2((Random.value - 0.5f) * dropForce, dropForce);
                rb.AddTorque((Random.value - 0.5f) * dropRotation);
                weapon.SetParent(droppedWeapons);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
