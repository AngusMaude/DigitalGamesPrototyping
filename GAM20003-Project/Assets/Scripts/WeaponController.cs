using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject player;
    private Vector2 aimInput = Vector2.zero;
    private bool shooting = false;

    [SerializeField] private float offset;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireRate;
    private float fireRateTimer;
    private string controlScheme;


    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.parent.gameObject;
        controlScheme = player.GetComponent<PlayerInput>().currentControlScheme;
    }

    public void OnAim(InputValue value) {
        if (controlScheme == "Controller")
            aimInput = value.Get<Vector2>();
        else if (controlScheme == "KeyboardMouse") {
            aimInput = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;
            Debug.LogError(transform.position);
        }
        else
            Debug.LogError("Control Scheme not found" + controlScheme);
    }
    
    public void OnShoot(InputValue value) {
        if (value.isPressed)
            shooting = true;
        else
            shooting = false;
        
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

        if (shooting && (fireRateTimer <= 0)) {
            Instantiate(projectile, firePoint.position, transform.rotation);
            fireRateTimer = fireRate;
        }
        fireRateTimer -= Time.deltaTime;
    }
}
