using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{

    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;
    private Animator animator;

    public int extraJumpsValue = 1;
    private int extraJumps;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }


    void Update()
    {
        // Reset extra jumps when landing (when transitioning from air to ground)
        if (isGrounded && !wasGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            { 
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 2);
            }
            else
            {
                if(extraJumps > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * 2);
                    extraJumps--;
                }
            }
           
        }
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        
        
        if (groundCheck != null)
        {
            wasGrounded = isGrounded;
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        setAnimation(moveHorizontal);
    }
    private void setAnimation(float moveHorizontal){
        if(isGrounded){
            if(moveHorizontal == 0){
                animator.Play("Player_idle");
            }
            else{
                animator.Play("Player_run");
            }

        }
        else{
            if(rb.velocity.y > 0){
                animator.Play("Player_jump");
            }
            else{
                animator.Play("Player_fall");
            }


        }
    }

}
