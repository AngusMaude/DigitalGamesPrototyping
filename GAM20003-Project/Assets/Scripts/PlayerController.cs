using UnityEngine;
using UnityEngine.InputSystem;
using System;

//TODO: wall jump
//TODO: dash
public class PlayerController : MonoBehaviour {

    [SerializeField] private float wallTimeout = 0.2f;
    [SerializeField] private float dashTimeout = 0.2f;
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 20.0f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask terrain;

    private Vector2 playerVelocity;

    private Vector2 movementInput = Vector2.zero;
    private float controlFreeze = 0;
    private bool jumped = false;
    private bool isGrounded = false;
    private bool dashing = false;
    private bool canDash = false;

    enum Wall {
        Left,
        Right,
        None
    }
    private Wall wall = Wall.None;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started)
            jumped = true;
    }

    private void GroundedCheck() {
        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, terrain);
    }

    private void WallCheck() {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, 0.1f, terrain))
            wall = Wall.Left;
        else if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, 0.1f, terrain))
            wall = Wall.Right;
        else
            wall = Wall.None;
    }

    void Update() {
        GroundedCheck();
        WallCheck();

        playerVelocity = rb.velocity;

        if (controlFreeze <= 0) {
            playerVelocity.x = movementInput.x * playerSpeed;
            if (dashing) {
                playerVelocity.y = rb.velocity.y * 0.25f;
                dashing = false;
            }
        }

        if (isGrounded)
            canDash = true;

        if (jumped) {
            if (isGrounded)
                playerVelocity.y = jumpHeight;
            else {
                switch (wall) {
                    case Wall.Left:
                        playerVelocity = new Vector2(playerSpeed, jumpHeight);
                        controlFreeze = wallTimeout;
                        break;
                    case Wall.Right:
                        playerVelocity = new Vector2(-playerSpeed, jumpHeight);
                        controlFreeze = wallTimeout;
                        break;
                    case Wall.None:
                        if (canDash) {
                            float mag = (float)Math.Sqrt(Math.Pow(movementInput.x, 2) + Math.Pow(movementInput.y, 2));
                            playerVelocity.x = (movementInput.x / mag) * playerSpeed * 3;
                            playerVelocity.y = (movementInput.y / mag) * playerSpeed * 3;
                            controlFreeze = dashTimeout;
                            canDash = false;
                            dashing = true;
                        }
                        break;
                }
            }
            jumped = false;
        }

        rb.velocity = playerVelocity;
        controlFreeze -= Time.deltaTime;
    }
}
