using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    public float moveSpreed;
    public float maxSpeed;
    public float jumpForce;
    public Transform groundCheck;
    
    internal Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool jump = false;
    private bool grounded = false;

    private Animator anim;
    private GravitySwitch gravitySwitch;
    private GameObject gravityGameObject;

    private int jumpHash;
    private int runStateHash;
    private float horizontalAxis;
    private bool isNearSwitch;

    private void Awake() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        gravityGameObject = GameObject.FindWithTag("GravitySwitch");
        if (gravityGameObject != null) {
            gravitySwitch = gravityGameObject.GetComponent<GravitySwitch>();
        }
    }

    // Check if player is grounded
    private void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (!LevelHandler.Fading) {
            if (Input.GetButtonDown("Jump") && grounded) {
                jump = true;
                rb2d.AddForce(new Vector2(rb2d.gravityScale, jumpForce));
            }
            if (Input.GetButtonDown("Fire1") && isNearSwitch) {
                gravitySwitch.ToggleSwitch();
                FlipGravity();
            }
        }
    }

    public void FlipGravity() {
        MainPlayer player = GameObject.FindWithTag("Player").GetComponent<MainPlayer>();
        MirrorPlayer mirrorPlayer = GameObject.FindWithTag("MirrorPlayer").GetComponent<MirrorPlayer>();

        // Reverse gravity
        player.transform.Rotate(new Vector3(180f, 0f, 0f));
        player.rb2d.gravityScale *= -1;
        player.jumpForce *= -1;

        mirrorPlayer.transform.Rotate(new Vector3(180f, 0f, 0f));
        mirrorPlayer.rb2d.gravityScale *= -1;
        mirrorPlayer.jumpForce *= -1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "GravitySwitch") {
            isNearSwitch = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "GravitySwitch") {
            isNearSwitch = false;
        }
    }

    // Calculate velocity for moving the player
    private void FixedUpdate() {
        if (!LevelHandler.Fading) {
            horizontalAxis = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveSpreed * horizontalAxis, rb2d.velocity.y);
            SetAnimations(horizontalAxis);
        } else {
            horizontalAxis = 0f;
            rb2d.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0f);
        }
    }

    // Set the animations for moving, jumping and turning
    private void SetAnimations(float horizontalAxis) {
        anim.SetFloat("Speed", Mathf.Abs(horizontalAxis));
        
        if (jump) { 
            anim.SetTrigger("Jump");
            jump = false;
        }

        if (horizontalAxis < 0 && facingRight || horizontalAxis > 0 && !facingRight) {
            if (grounded) {
                anim.SetTrigger("Turn");
            }
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            
        }
    }

}