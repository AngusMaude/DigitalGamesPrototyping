using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour {

    private Player player;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private float groundFriction;
    public AudioSource PlayerControllerAudio;
    public AudioClip[] DashClips;
    public AudioClip[] JumpClips;

    private Vector2 movementInput = Vector2.zero;
    private Vector2 aimInput = Vector2.zero;
    private bool jumpInput = false;
    private bool dashInput = false;
    private bool dashing = false;
    private int dashCount = 0;
    private int jumpCount = 0;
    private bool isGrounded = false;


    private float dashT;

    enum Wall {
        Left,
        Right,
        None
    }
    private Wall wall = Wall.None;

    private void Start() {
        DontDestroyOnLoad(gameObject);

        player = GetComponent<Player>();
        GameManager.instance.AddPlayer(player);
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
            jumpCount = player.GetStats().GetJumpCount();
        }
        if (wall != Wall.None)
            jumpCount = player.GetStats().GetJumpCount();
        if (jumpInput) {
            if (jumpCount > 0) {
                player.GetRigidbody().velocity = new Vector2(player.GetRigidbody().velocity.x, 0);
                switch (wall) {
                    case Wall.Left:
                        player.GetRigidbody().AddForce(new Vector2(player.GetStats().GetJumpHeight(), player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
                        break;
                    case Wall.Right:
                        player.GetRigidbody().AddForce(new Vector2(-player.GetStats().GetJumpHeight(), player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
                        break;
                    case Wall.None:
                        player.GetRigidbody().AddForce(new Vector2(0, player.GetStats().GetJumpHeight()), ForceMode2D.Impulse);
                        break;
                }
                if (JumpClips.Length > 0) {
                    //PlayerControllerAudio.clip = JumpClips[UnityEngine.Random.Range(0, JumpClips.Length)];
                    //PlayerControllerAudio.Play(0);
                    AudioSource.PlayClipAtPoint(JumpClips[UnityEngine.Random.Range(0, JumpClips.Length)], player.GetCollider().bounds.center);
                }
                jumpCount--;
            }
            jumpInput = false;
        }
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
            if (DashClips.Length > 0) {
                //PlayerControllerAudio.clip = DashClips[UnityEngine.Random.Range(0, DashClips.Length)];
                //PlayerControllerAudio.Play(0);
                AudioSource.PlayClipAtPoint(DashClips[UnityEngine.Random.Range(0, DashClips.Length)], player.GetCollider().bounds.center);
            }
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

        Vector2 moveForce = (movementInput * player.GetStats().GetMaxSpeed()) - player.GetRigidbody().velocity;
        moveForce *= player.GetStats().GetAcceleration();

        if (isGrounded) {
            moveForce *= groundFriction;
        }

        moveForce.y = 0;
        player.GetRigidbody().AddForce(moveForce);
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
