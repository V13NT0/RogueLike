using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public static playerController instance;

    [Header("Movimiento")]
    public float moveSpeed;

    [Header("Ataque")]
    public int attackingDebuff;
    private float result;

    [Header("Salto")]
    private bool canDoubleJump;
    public float jumpForce;

    [Header("Componente")]
    public Rigidbody2D rb;

    [Header("Animator")]
    public Animator anim;

    public Boolean isFlip;
    public SpriteRenderer sr;
    

    [Header("Ground Check")]
    private bool isGrounded;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;

    public float knockBackLengh, knockBackForce;
    private float knockBackCount;

    public bool stopInput;

    public Transform hitPoint;

    //private bool isMoving;


    private void Awake() {
        instance = this;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //sr = GetComponentInChildren<SpriteRenderer>();
        isFlip = false;
        //isMoving = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseMenu.instance.isPaused && !stopInput)
        {

            if(knockBackCount <= 0)
            {
                

                if(Input.GetButton("Fire2") && isGrounded)
                {
                    
                    anim.SetBool("blocking", true);

                    rb.velocity = new Vector2(0f, rb.velocity.y);

                    
                } else 
                {
                    anim.SetBool("blocking", false);

                    rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);

                    isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround);

                    if(isGrounded) canDoubleJump = true;

                    if(Input.GetButtonDown("Jump"))
                    {
                        if(isGrounded)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                            audioManager.instance.PlaySSFX(0);

                        }else 
                        {
                            if(canDoubleJump)
                            {
                                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                                canDoubleJump = false;
                                audioManager.instance.PlaySSFX(0);
                            }

                        }   
                        
                    }



                    /* if(isGrounded)
                    {
                        if(rb.velocity.x != 0)
                        {
                            isMoving = true;                
                        } else
                        {
                            isMoving = false;
                        }
                    }  */

                    //if(isMoving) audioManager.instance.PlaySSFX(1);


                    playerCombat.instance.Combo();

                    if(playerCombat.instance.attacking)
                    {
                        result = moveSpeed / attackingDebuff;
                        rb.velocity = new Vector2(result * Input.GetAxisRaw("Horizontal"), rb.velocity.y);

                    } //else rb.velocity = new Vector2(moveSpeed / Input.GetAxisRaw("Horizontal"), rb.velocity.y);

                    if(rb.velocity.x < 0) 
                    {
                        //sr.flipX = true;
                        //transform.localScale = new Vector3(-1, 1, 1);
                        transform.rotation = Quaternion.Euler(0,180,0);
                        isFlip = true;

                    } else if(rb.velocity.x > 0)
                    {
                        //sr.flipX = false;
                        //transform.localScale = new Vector3(1, 1, 1);
                        transform.rotation = Quaternion.Euler(0,0,0);
                        isFlip = false;
                    }
                
                    if (!isGrounded)
                    {
                        anim.SetFloat("jumpSpeed", rb.velocity.y);
                    } else anim.SetFloat("jumpSpeed", 0);


                }



            } else
            {
                knockBackCount -= Time.deltaTime;
                if(!isFlip)
                {
                    rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                }else
                {
                    rb.velocity = new Vector2(knockBackForce, rb.velocity.y);
                }
            }

        }

        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        //anim.SetBool("falling", falling);

        
    }

    public void KnockBack()
    {

        knockBackCount = knockBackLengh;
        rb.velocity = new Vector2(0f, knockBackForce);

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

}
