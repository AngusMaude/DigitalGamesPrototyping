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
    [SerializeField] protected float knockback;
    [SerializeField] protected float bloomMaximum;
    [SerializeField] protected float bloomAccumulationRate;
    [SerializeField] protected float bloomDecayRate;
    [SerializeField] protected float bloomMinimum;
    [SerializeField] protected int magSize;
    protected int magAmmo;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected int reserveAmmo;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected bool infiniteAmmo;

    protected float weaponCooldown;
    protected bool shooting = false;
    protected bool reloading = false;
    private float currentBloom = 0f; // running accumulation of current bloom

    // Particles 
    public GameObject particleHitPrefab;
    public GameObject particleFirePrefab;

    // Audio
    public AudioSource WeaponAudio;
    public AudioClip[] AudioClips;
    public AudioClip ReloadClip;
    public AudioClip DryFireClip;

    // Start is called before the first frame update
    void Start() {
        WeaponAudio = GetComponent<AudioSource>();
        magAmmo = magSize;

        string parent = transform.parent.name;
        if (parent.Contains(" ("))
            parent = parent.Substring(0, parent.IndexOf(" ("));

        switch (parent) {
            case "WeaponHandler":
                GetComponent<Rigidbody2D>().simulated = false;
                break;

            case "DroppedWeapons":
                GetComponent<Rigidbody2D>().simulated = true;
                this.enabled = false;
                break;
            case "WeaponSpawner":
                GetComponent<Rigidbody2D>().simulated = false;
                this.enabled = false;
                break;
            default:
                Debug.LogError("Weapon parent not recognised: " + transform.parent.name);
                break;
        }
        if (player != null){
            player.UpdateUIAmmoCount(magSize, magAmmo);
        }
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

        if (reloading){
            Reload();
            player.UpdateUIReloadTimer(reloadTime * player.GetStats().GetReloadTime(), weaponCooldown);
            player.UpdateUIAmmoCount(magSize, magAmmo);
        }
        // flip player depending on aiming direction
        if (aimInput.x > 0.1)
            player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (aimInput.x < -0.1)
            player.transform.eulerAngles = new Vector3(0f, 180, 0f);


        Debug.DrawRay(this.transform.position, new Vector3(aimInput.x * 5f, aimInput.y * 5f, 0f), Color.blue, 0f, false);
        // Debug show bloom range
        float tempMax = 0.5f * currentBloom * Mathf.Deg2Rad;
        float tempMin = -0.5f * currentBloom * Mathf.Deg2Rad;
        Debug.DrawRay(firePoint.position, new Vector3((aimInput.x * Mathf.Cos(tempMax) - aimInput.y * Mathf.Sin(tempMax)) * 10f, (aimInput.x * Mathf.Sin(tempMax) + aimInput.y * Mathf.Cos(tempMax)) * 10f, 0f), Color.yellow, 0f, false);
        Debug.DrawRay(firePoint.position, new Vector3((aimInput.x * Mathf.Cos(tempMin) - aimInput.y * Mathf.Sin(tempMin)) * 10f, (aimInput.x * Mathf.Sin(tempMin) + aimInput.y * Mathf.Cos(tempMin)) * 10f, 0f), Color.yellow, 0f, false);


        float rotZ = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, rotZ);

        if (shooting && (weaponCooldown <= 0)) {

            if (magAmmo <= 0) {
                weaponCooldown = reloadTime * player.GetStats().GetReloadTime();
                reloading = true;
                if (DryFireClip != null) {
                    WeaponAudio.clip = DryFireClip;
                    WeaponAudio.Play(0);
                }
            }
            else {

                Shoot();

                // Play audio
                if (AudioClips.Length > 0) {
                    WeaponAudio.clip = AudioClips[Random.Range(0, AudioClips.Length)];
                    WeaponAudio.Play(0);
                }

                weaponCooldown = fireRate;
                // Accumulate bloom after firing
                currentBloom += bloomAccumulationRate;
                if (semiAutomatic)
                    shooting = false;

                magAmmo -= 1;
                player.UpdateUIAmmoCount(magSize, magAmmo);
            }
        }
        else {
            weaponCooldown -= Time.deltaTime;
            currentBloom = currentBloom - (Time.deltaTime * bloomDecayRate);
        }
        currentBloom = Mathf.Clamp(currentBloom, bloomMinimum, bloomMaximum * player.GetStats().GetBloom());
    }

    protected virtual void Shoot() {
        HitScanShot();
    }

    protected void HitScanShot() {

        float bloom = ((Random.value - 0.5f) * currentBloom * Mathf.Deg2Rad);
        Vector2 bloomAim = aimInput;

        bloomAim.x = aimInput.x * Mathf.Cos(bloom) - aimInput.y * Mathf.Sin(bloom);
        bloomAim.y = aimInput.x * Mathf.Sin(bloom) + aimInput.y * Mathf.Cos(bloom);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, bloomAim);
        Debug.DrawRay(firePoint.position, new Vector3(bloomAim.x * 10f, bloomAim.y * 10f, 0f), Color.red, 1f, false);
        if (hit.collider != null) {
            if (hit.transform.name != "Terrain") {
                hit.rigidbody.AddForce(aimInput * knockback * player.GetStats().GetKnockback(), ForceMode2D.Impulse);

                //probably a better way to do this
                if (hit.transform.name == "Player(Clone)") {
                    hit.transform.GetComponent<Player>().Hit(baseDamage);
                }
            }

            Instantiate(particleHitPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            Instantiate(particleFirePrefab, firePoint.position, Quaternion.FromToRotation(Vector3.forward, bloomAim));
        }
    }

    protected virtual void Reload() {
        if (weaponCooldown <= 0) {
            reloading = false;
            if (reserveAmmo > magSize) {
                magAmmo = magSize;
                if (!infiniteAmmo)
                    reserveAmmo -= magSize;
            }
            else {
                magAmmo = reserveAmmo;
                reserveAmmo = 0;
            }
            player.UpdateUIReloadTimer(reloadTime * player.GetStats().GetReloadTime(), 0);
            if (ReloadClip != null) {
                WeaponAudio.clip = ReloadClip;
                WeaponAudio.Play(0);
            }
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
        if (value.isPressed && reloading == false)
            shooting = true;
        else {
            shooting = false;
        }
    }

    public void OnReload() {
        weaponCooldown = reloadTime * player.GetStats().GetReloadTime();
        reloading = true;

    }

    public bool CheckEmpty() { return (magAmmo <= 0) && (reserveAmmo <= 0); }

    public void SetInfiniteAmmo(bool value) { infiniteAmmo = value; }
}
