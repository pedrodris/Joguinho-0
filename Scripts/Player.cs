using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpingForce;
    public float DashForce;

    private Rigidbody2D rig;
    private Animator anim;

    public bool isJumping;
    public bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower; 
    public float dashingTime;
    public float dashingCoolDown;

    private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        
    }

    void Move ()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f,0f);
        transform.position += movement * Time.deltaTime * Speed; 
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("run", false);
        }
        
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") )
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpingForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);

            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpingForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
    }

   // void Dash()
    //{
      //  if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        //{
          //  canDash = false;
            //isDashing = true/;
//            float originalGravity = rb.gravityScale;
  //          rb.gravityScale = 0f;
    //        rb.velocity = new Vector2(transform.localScale.x * dashingPower,0f);
      //      rb.emitting = true;
        //    yield return new WaitForSeconds(dashingTime);
          //  tr.emitting = false;
            //rb.gravityScale = originalGravity;
            //isDashing = false;
            //yield return new WaitForSeconds(dashingCoolDown);
            //canDash = true;
        //}
    //}

   


    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);

        }
        
        if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);

        }
        if(collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);

        }

        
    }
    
     void OnCollisionExit2D(Collision2D collision)
    {
         if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

}


