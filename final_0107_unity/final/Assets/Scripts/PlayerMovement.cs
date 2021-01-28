using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;

    Animator anim;

    public CharacterController2D controller;
    public float runSpeed = 35f;
    float horizontalMove = 0f;
    bool jump = false;

    string horizontal;
    string jumpKey;

    private void Start()
    {
        anim = GetComponent<Animator>();

        jump = false;

        if (this.gameObject.name == "p1")
        {
            horizontal = "Horizontal";
            jumpKey = "Jump";
        }
        else
        {
            horizontal = "Horizontal2";
            jumpKey = "Jump2";
        }
    }

    void Update()
    {
        if(Time.timeScale==1)horizontalMove = Input.GetAxisRaw(horizontal) * runSpeed;
        else horizontalMove = 0;
        if (horizontalMove != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if(Input.GetButtonDown(jumpKey) && Time.timeScale==1)
        {
            jump = true;
        }
        
    }

    void FixedUpdate()
    {
        anim.SetBool("isThrowing", false);
        if (((camera.transform.position.x > this.gameObject.transform.position.x - 8) && (camera.transform.position.x < this.gameObject.transform.position.x + 8)) ||
           (camera.transform.position.x < this.gameObject.transform.position.x - 8 && horizontalMove < 0) ||
           (camera.transform.position.x > this.gameObject.transform.position.x + 8 && horizontalMove > 0))
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        else controller.Move(0, jump);
        jump = false;
    }
}
