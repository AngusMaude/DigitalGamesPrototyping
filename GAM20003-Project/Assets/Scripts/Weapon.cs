using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Player player;
    protected Vector2 aimInput = Vector2.zero;

    [SerializeField] protected Transform firePoint;
    [SerializeField] protected bool semiAutomatic = false;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float bloomAngle;
    [SerializeField] protected int magSize;
    protected int magAmmo;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected int reserveAmmo;
    [SerializeField] protected float baseDamage;

    protected float weaponCooldown;
    protected bool shooting = false;
    protected bool reloading = false;


    // Start is called before the first frame update
    void Start()
    {
        magAmmo = magSize;
        if (transform.parent.name == "WeaponHandler") {
            GetComponent<Rigidbody2D>().simulated = false;
        }
        else if (transform.parent.name == "SceneWeapons") {
            GetComponent<Rigidbody2D>().simulated = true;
            this.enabled = false;
        }
        else
            Debug.LogError("Weapon parent not recognised: " + transform.parent.name);
    }

    private void OnEnable() {
        shooting = false;
        UpdatePlayer();
    }

    private void UpdatePlayer() {
        if (transform.parent.name == "WeaponHandler") {
            player = this.transform.parent.parent.gameObject.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        // flip player depending on aiming direction
        if (aimInput.x > 0.1)
            player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (aimInput.x < -0.1)
            player.transform.eulerAngles = new Vector3(0f, 180, 0f);


        Debug.DrawRay(this.transform.position, new Vector3(aimInput.x * 5f, aimInput.y * 5f, 0f), Color.red, 0f, false);

        float rotZ = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, rotZ);

        Shoot();
    }

    protected virtual void Shoot() {
        if (shooting && (weaponCooldown <= 0)) {
            if (reloading)
                Reload();

            float bloom = (Random.value - 0.5f) * bloomAngle * player.GetStats().GetBloom();

            if (magAmmo <= 0) {
                weaponCooldown = reloadTime * player.GetStats().GetReloadTime();
                reloading = true;
            }
            else {
                //Instantiate(projectile, firePoint.position, transform.rotation * Quaternion.Euler(Vector3.forward * bloom));
                //TODO:: ADD BLOOM
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, aimInput);

                if (hit.collider != null) {
                    if (hit.transform.name != "Terrain") {
                        //TODO:: REDO MOVEMENT TO BE FORCE BASED INSTEAD OF DISPLACEMENT BASED FOR KNOCKBACK
                        //hit.rigidbody.AddForce(aimInput * 100);

                        //probably a better way to do this
                        if (hit.transform.name == "Player(Clone)") {
                            hit.transform.GetComponent<Player>().Hit(baseDamage);
                        }
                    }
                }

                weaponCooldown = fireRate;
                if (semiAutomatic)
                    shooting = false;

                magAmmo -= 1;
            }
        }
        weaponCooldown -= Time.deltaTime;
    }

    protected void Reload() {
        Debug.Log("reload");
        reloading = false;
        if (reserveAmmo > magSize) {
            magAmmo = magSize;
            reserveAmmo -= magSize;
        }
        else {
            magAmmo = reserveAmmo;
            reserveAmmo = 0;
        }
    }




    // Input handlers
    public void OnAim(InputValue value) {
        //TODO::probably should be switch statement
        if (player.GetControlScheme() == "Controller")
            aimInput = value.Get<Vector2>();
        else if (player.GetControlScheme() == "KeyboardMouse")
            aimInput = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;
        else
            Debug.LogError("Control Scheme not found" + player.GetControlScheme());

        aimInput.Normalize();
    }

    public void OnShoot(InputValue value) {
        if (value.isPressed)
            shooting = true;
        else
            shooting = false;
    }

    public void OnReload() {
        weaponCooldown = reloadTime;
        reloading = true;
    }
}
