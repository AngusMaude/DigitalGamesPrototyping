using UnityEngine;
using UnityEngine.InputSystem;

//TODO: wall jump
//TODO: dash
public class PlayerController : MonoBehaviour {
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 15.0f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask terrain;

    private Vector2 playerVelocity;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool isGrounded = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumped = context.action.triggered;
    }

    private void GroundedCheck() {
        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, terrain);
    }

    void Update() {
        GroundedCheck();
        playerVelocity = new Vector2(movementInput.x * playerSpeed, rb.velocity.y);

        // TODO: stop multiple jumps
        if (jumped && isGrounded) {
            playerVelocity.y = jumpHeight;
        }

        rb.velocity = playerVelocity;
    }
}
