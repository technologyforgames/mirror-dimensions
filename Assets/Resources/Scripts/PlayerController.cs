using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    public Transform groundCheck;


    internal bool facingRight = true;
    internal bool jump = false;
    internal Rigidbody2D rb2d;


    private bool grounded = false;
    private Animator anim;
    

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown("up") && grounded) {
            jump = true;
        }
    }


    private void FixedUpdate() {
        float horizontalAxis = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveSpeed * horizontalAxis, rb2d.velocity.y);
        SetAnimations(horizontalAxis);
    }

    private void SetAnimations(float horizontalAxis) {
        if (horizontalAxis > 0 && facingRight) {
            anim.SetBool("Turn", false);
            anim.SetBool("Moving", true);
        } else if (horizontalAxis < 0 && !facingRight) {
            anim.SetBool("Turn", false);
            anim.SetBool("Moving", true);
        } else {
            anim.SetBool("Moving", false);
        }

        if (jump) {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (horizontalAxis > 0 && !facingRight) {
            Flip();
        } else if (horizontalAxis < 0 && facingRight) {
            Flip();
        }
    }

    private void Flip() {
        anim.SetTrigger("Turn");
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}