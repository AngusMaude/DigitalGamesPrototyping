using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected GameObject player;
    protected Vector2 aimInput = Vector2.zero;
    protected bool shooting = false;

    [SerializeField] protected float offset;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform firePoint;

    [SerializeField] protected float fireRate;
    protected float fireRateTimer;
    protected string controlScheme;


    // Start is called before the first frame update
    void Start()
    {
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
        player = this.transform.parent.parent.gameObject;
        controlScheme = player.GetComponent<PlayerInput>().currentControlScheme;
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
        if (shooting && (fireRateTimer <= 0)) {
            Instantiate(projectile, firePoint.position, transform.rotation);
            fireRateTimer = fireRate;
        }
        fireRateTimer -= Time.deltaTime;
    }


    public void OnAim(InputValue value) {
        if (controlScheme == "Controller")
            aimInput = value.Get<Vector2>();
        else if (controlScheme == "KeyboardMouse")
            aimInput = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;
        else
            Debug.LogError("Control Scheme not found" + controlScheme);
    }

    public void OnShoot(InputValue value) {
        if (value.isPressed)
            shooting = true;
        else
            shooting = false;

    }
}
