using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour {

    private Player player;
    [SerializeField] private LayerMask terrain;

    private Vector2 movementInput = Vector2.zero;
    private bool jump = false;
    private bool dash = false;
    private bool dashing = false;
    private int dashCount = 0;
    private bool isGrounded = false;

    private float dashT;

    enum Wall {
        Left,
        Right,
        None
    }
    private Wall wall = Wall.None;

    private void Start() {
        player = GetComponent<Player>();
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
        if (jump) {
            if (isGrounded)
                player.GetRigidbody().AddForce(new Vector2(0, player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
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
            jump = false;
        }
    }

    private void Dash() {
        if (isGrounded && !dashing)
            dashCount = player.GetStats().GetDashCount();

        if ((dashCount > 0) && dash) {
            Vector2 dashForce = Vector2.zero;
            dashForce.x = movementInput.x * player.GetStats().GetDashSpeed();
            dashForce.y = movementInput.y * player.GetStats().GetDashSpeed();

            player.GetRigidbody().velocity = Vector2.zero;
            player.GetRigidbody().AddForce(dashForce, ForceMode2D.Impulse);

            dashT = player.GetStats().GetDashTime();
            dashing = true;
            dashCount -= 1;
        }
        dash = false;

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
        jump = true;
    }

    public void OnDash() {
        dash = true;
    }
}
