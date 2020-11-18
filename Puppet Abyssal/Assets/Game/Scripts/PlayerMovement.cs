using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public bool isJumping;
    public bool isGrounded;
    public bool doubleJump;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb2d;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horMovement;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }




    private void Update()
    {
        horMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;

        if (Input.GetButton("Jump") && isGrounded)
        {
            isJumping = true;
        }

        if (isGrounded)
        {
            doubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && doubleJump)
        {
            isJumping = true;
            doubleJump = false;
        }

        Flip(rb2d.velocity.x);

        float characterVelocity = Mathf.Abs(rb2d.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        
        MovePlayer(horMovement);
    }

    void MovePlayer(float _horMovement)
    {
        Vector3 targetVelocity = new Vector2(_horMovement, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, 0.1f);
    
        if(isJumping == true)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1F)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
