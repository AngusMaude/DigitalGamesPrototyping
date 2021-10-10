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
    
    // Start is called before the first frame update

    public void ChangeScene() {
        foreach (Transform weapon in transform) {
            if (weapon != defaultWeapon.transform)
                Destroy(weapon.gameObject);
        }
        sceneWeapons = GameObject.Find("SceneWeapons");
        droppedWeapons = sceneWeapons.transform.Find("DroppedWeapons");
        defaultWeapon = Instantiate(defaultWeapon, transform);
        defaultWeapon.GetComponent<Weapon>().SetInfiniteAmmo(true);
    }

    void OnPickUp() {
        foreach (Transform container in sceneWeapons.transform) {
            foreach (Transform weapon in container) {
                if (Vector3.Distance(transform.position, weapon.position) < weaponReach) {
                    Drop();
                    PickUp(weapon);
                    weapon.GetComponent<Weapon>().enabled = true;
                    return;
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
        if (transform.GetChild(0) != defaultWeapon) {
            Drop();
        }
    }

    void Drop() {
        foreach (Transform weapon in transform) {
            if (weapon != defaultWeapon.transform) {
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
        if (transform.childCount > 1)
            defaultWeapon.SetActive(false);
        else
            defaultWeapon.SetActive(true);
        

        foreach (Transform weapon in transform) {
            if (weapon.GetComponent<Weapon>().CheckEmpty()) {
                Drop();
                defaultWeapon.SetActive(true);
                Destroy(weapon.gameObject, 3);
                //TODO:: add sound for empty weapon
            }
        }
    }
}
