using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;

    private Rigidbody2D rb;

    private Vector2 playerVelocity;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumped = context.action.triggered;
    }

    void Update() {

        playerVelocity = new Vector2(movementInput.x * playerSpeed, rb.velocity.y);

        if (jumped) {
            playerVelocity.y = jumpHeight;
        }

        rb.velocity = playerVelocity;
    }
}
