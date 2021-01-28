using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 800f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.

    
    public bool FacingRight = true;
    public bool dead = false;

    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Rigidbody2D m_Rigidbody2D;
    private Animator anim;
    
    private Vector3 m_Velocity = Vector3.zero;

    public Camera camera;
    public AudioSource SoundEffect;
    public AudioClip deadSE;
    public Animator animInstruction;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dead = false;
    }

    private void Update()
    {
        if(dead)
        {
            SoundEffect.PlayOneShot(deadSE);
        }
        
        if(!this.gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            animInstruction.SetTrigger("appear");
            StartCoroutine(Reborn());
        }

    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        
    }


    public void Move(float move, bool jump)
    {
        anim.SetBool("isJumping", false);

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            
        if (move > 0 && !FacingRight)
        {
            Flip();
        }
        else if (move < 0 && FacingRight)
        {
            Flip();
        }
        
        if (m_Grounded && jump)
        {
            anim.SetBool("isJumping", true);
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        FacingRight = !FacingRight;
        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator Reborn()
    {
        dead = false;

        if (this.gameObject.name == "p1")
        {
            this.gameObject.transform.position = new Vector3(Screen.width, -2.8f, 0);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(-Screen.width, -2.8f, 0);
        }
        m_Rigidbody2D.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1);

        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        this.gameObject.GetComponent<PickThrowController>().enabled = true;
        if (this.gameObject.name == "p1")
        {
            CameraController.p1_alive=true;
        }
        else
        {
            CameraController.p2_alive=true;
        }
        

        m_Rigidbody2D.velocity = new Vector2(0, 0);

        // not exceed both houses
        if (this.gameObject.name == "p1")
        {
            if(camera.transform.position.x + 7.5f < 58f)
                this.gameObject.transform.position = new Vector3(camera.transform.position.x + 7.5f, 6.0f, 0);
            else
                this.gameObject.transform.position = new Vector3(58f, 6.0f, 0);

            this.gameObject.transform.localScale = new Vector3(1, 1, 0);
            FacingRight = false;
        }
        else
        {
            if (camera.transform.position.x - 7.5f > -58f)
                this.gameObject.transform.position = new Vector3(camera.transform.position.x - 7.5f, 6.0f, 0);
            else
                this.gameObject.transform.position = new Vector3(-58f, 6.0f, 0);

            this.gameObject.transform.localScale = new Vector3(-1,1,0);

            FacingRight = true;
            
        }
        CameraController.Dominance = 0;
    }

}