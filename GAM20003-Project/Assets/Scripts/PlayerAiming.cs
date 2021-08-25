using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{

    private Vector2 aimInput = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnAim(InputAction.CallbackContext context) {
        aimInput = context.ReadValue<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        if (aimInput.x > 0)
            transform.eulerAngles = Vector3.zero;
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
