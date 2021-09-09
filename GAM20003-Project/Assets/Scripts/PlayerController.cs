using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float wallTimeout = 0.2f;
    [SerializeField] private float dashTimeout = 0.2f;

    private Player player;
    [SerializeField] private LayerMask terrain;

    private Vector2 playerVelocity;

    private Vector2 movementInput = Vector2.zero;
    private float controlFreeze = 0;
    private bool jump = false;
    private bool dash = false;
    private bool dashing = false;
    private bool canDash = false;
    private bool isGrounded = false;



    enum Wall {
        Left,
        Right,
        None
    }
    private Wall wall = Wall.None;

    private void Start() {
        player = GetComponent<Player>();
    }

    public void OnMovement(InputValue value) {
        movementInput = value.Get<Vector2>();
    }
    
    public void OnJump() {
        jump = true;
    }
    
    public void OnDash() {
        dash = true;
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

    void Update() {
        GroundedCheck();
        WallCheck();

        playerVelocity = player.GetRigidbody().velocity;

        if (controlFreeze <= 0) {
            playerVelocity.x = movementInput.x * player.GetStats().GetMoveSpeed() ;
            if (dashing) {
                playerVelocity.y = player.GetRigidbody().velocity.y * 0.25f;
                dashing = false;
                canDash = false;
            }
        }

        if (isGrounded)
            canDash = true;

        if (jump) {
            if (isGrounded)
                playerVelocity.y = player.GetStats().GetJumpHeight();
            else {
                switch (wall) {
                    case Wall.Left:
                        playerVelocity = new Vector2(player.GetStats().GetMoveSpeed(), player.GetStats().GetJumpHeight());
                        controlFreeze = wallTimeout;
                        break;
                    case Wall.Right:
                        playerVelocity = new Vector2(-player.GetStats().GetMoveSpeed(), player.GetStats().GetJumpHeight());
                        controlFreeze = wallTimeout;
                        break;
                }
            }
            jump = false;
        }

        if (canDash && dash) {
            float mag = (float)Math.Sqrt(Math.Pow(movementInput.x, 2) + Math.Pow(movementInput.y, 2));
            if (mag != 0) {
                playerVelocity.x = (movementInput.x / mag) * player.GetStats().GetMoveSpeed() * player.GetStats().GetDashSpeed();
                playerVelocity.y = (movementInput.y / mag) * player.GetStats().GetMoveSpeed() * player.GetStats().GetDashSpeed();
                controlFreeze = dashTimeout;
                dashing = true;
                canDash = false;
            }
        }
        dash = false;


        player.GetRigidbody().velocity = playerVelocity;
        controlFreeze -= Time.deltaTime;
    }


}
