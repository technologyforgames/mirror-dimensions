using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveSpreed;
    public float maxSpeed;
    public float jumpForce;
    public Transform groundCheck;


    internal bool facingRight = true;
    internal bool jump = false;
    internal Rigidbody2D rb2d;


    private bool grounded = false;
    private Animator anim;
    private int jumpHash;
    private int runStateHash;


    private void Awake() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Check if player is grounded
    private void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
            rb2d.AddForce(new Vector2(rb2d.gravityScale, jumpForce));
        }
    }

    // Calculate velocity for moving the player
    private void FixedUpdate() {
        float horizontalAxis = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveSpreed * horizontalAxis, rb2d.velocity.y);
        SetAnimations(horizontalAxis);
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