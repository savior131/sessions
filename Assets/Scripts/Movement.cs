using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float acceleration;
    [SerializeField] LayerMask ground;

    Rigidbody2D rb;
    BoxCollider2D col;
    Animator anim;

    void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim= GetComponent<Animator>(); 
    }

    void Update()
    {
        MovementHandler();
        JumpHandler();
        AnimationHAndler();
    }
    private void MovementHandler()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float targetspeed = horizontal * speed;
        float currentspeed = rb.velocity.x;
        if(currentspeed < targetspeed)
        {
            currentspeed += acceleration * Time.deltaTime;
            if(currentspeed > targetspeed)
            {
                currentspeed = targetspeed;
            }
        }
        else if(currentspeed > targetspeed)
        {
            currentspeed -= acceleration * Time.deltaTime;
            if (currentspeed < targetspeed)
            {
                currentspeed = targetspeed;
            }
        }
        rb.velocity = new Vector2(currentspeed,rb.velocity.y);
    }
    private void JumpHandler()
    {
        if (Input.GetKeyDown(KeyCode.W)&& isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void AnimationHAndler()
    {
        if (rb.velocity.x != 0)
        {
            anim.SetBool("run",true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (!isGrounded())
        {
            anim.SetBool ("inAir", true);
        }
        else
        {
            anim.SetBool("inAir", false);
        }
    
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.2f, ground);
    }
}
