using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour {

    private Player player;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private float groundFriction;
    [SerializeField] private float coyoteTime;

    private Vector2 movementInput = Vector2.zero;
    private Vector2 aimInput = Vector2.zero;
    private bool jumpInput = false;
    private bool dashInput = false;
    private bool dashing = false;
    private int dashCount = 0;
    private bool isGrounded = false;

    private float dashT;
    private float canJumpT;

    enum Wall {
        Left,
        Right,
        None
    }
    private Wall wall = Wall.None;

    private void Start() {
        DontDestroyOnLoad(gameObject);

        player = GetComponent<Player>();
        GameManager.instance.AddPlayer(this);
    }

    private void GroundedCheck() {
        isGrounded = Physics2D.BoxCast(player.GetCollider().bounds.center, player.GetCollider().bounds.size, 0f, Vector2.down, 0.1f, terrain);
    }

    private void WallCheck() {
        if (Physics2D.BoxCast(player.GetCollider().bounds.center, player.GetCollider().bounds.size, 0f, Vector2.left, 0.1f, terrain))
            wall = Wall.Left;
        else if (Physics2D.BoxCast(player.GetCollider().bounds.center, player.GetCollider().bounds.size, 0f, Vector2.right, 0.1f, terrain))
            wall = Wall.Right;
        else
            wall = Wall.None;
    }

    private void Jump() {
        if (isGrounded) {
            canJumpT = coyoteTime;
        }
        if (jumpInput) {
            if (canJumpT > 0) {
                player.GetRigidbody().AddForce(new Vector2(0, player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
                
            }
            else {
                switch (wall) {
                    case Wall.Left:
                        player.GetRigidbody().velocity = Vector2.zero;
                        player.GetRigidbody().AddForce(new Vector2(player.GetStats().GetJumpHeight(), player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
                        break;
                    case Wall.Right:
                        player.GetRigidbody().velocity = Vector2.zero;
                        player.GetRigidbody().AddForce(new Vector2(-player.GetStats().GetJumpHeight(), player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
                        break;
                }
            }
            canJumpT = 0;
            jumpInput = false;
        }
        canJumpT -= Time.deltaTime;
    }

    private void Dash() {
        if (isGrounded && !dashing)
            dashCount = player.GetStats().GetDashCount();

        if ((dashCount > 0) && dashInput) {
            Vector2 dashForce = Vector2.zero;

            switch (player.GetControlScheme()) {
                case "Controller":
                    dashForce.x = movementInput.x * player.GetStats().GetMaxSpeed() * 3f;
                    dashForce.y = movementInput.y * player.GetStats().GetMaxSpeed() * 3f;
                    break;
                case "KeyboardMouse":
                    dashForce.x = aimInput.x * player.GetStats().GetMaxSpeed() * 3f;
                    dashForce.y = aimInput.y * player.GetStats().GetMaxSpeed() * 3f;
                    break;
                default:
                    break;
            }

            player.GetRigidbody().velocity = Vector2.zero;
            player.GetRigidbody().AddForce(dashForce, ForceMode2D.Impulse);

            dashT = player.GetStats().GetDashTime();
            dashing = true;
            dashCount -= 1;
        }
        dashInput = false;

        if (dashing) {
            dashT -= Time.deltaTime;
            if (dashT <= 0) {
                dashing = false;
                player.GetRigidbody().velocity *= 0.25f;
            }
        }
    }

    private void Move() {

        Vector2 moveForce = (movementInput * player.GetStats().GetAcceleration()) - player.GetRigidbody().velocity;
        //clamps velocity
        
        if ((player.GetRigidbody().velocity.x > player.GetStats().GetMaxSpeed()) && (moveForce.x > 0))
            moveForce.x = 0;
        else if ((player.GetRigidbody().velocity.x < -player.GetStats().GetMaxSpeed()) && (moveForce.x < 0))
            moveForce.x = 0;

        moveForce.y = 0;
        player.GetRigidbody().AddForce(moveForce);

        if (isGrounded && (movementInput == Vector2.zero)) {
            player.GetRigidbody().AddForce(player.GetRigidbody().velocity * -groundFriction);
        }

        if (wall != Wall.None) {
            float wallSpeed = player.GetStats().GetMaxSpeed() * player.GetStats().GetWallFriction();
            player.GetRigidbody().velocity = new Vector2(player.GetRigidbody().velocity.x, Mathf.Clamp(player.GetRigidbody().velocity.y, -wallSpeed, float.MaxValue));
        }
    }

    void Update() {
        GroundedCheck();
        WallCheck();
        
        Jump();
        Dash();
        Move();
    }

    public void OnMovement(InputValue value) {
        movementInput = value.Get<Vector2>();
        movementInput.Normalize();
    }

    public void OnJump() {
        jumpInput = true;
    }

    public void OnDash() {
        dashInput = true;
    }

    public void OnAim(InputValue value) {
        if (player && player.GetControlScheme() == "KeyboardMouse")
            aimInput = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;

        aimInput.Normalize();
    }
}
