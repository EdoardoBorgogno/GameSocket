using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    bool isPlayer;

    public float runSpeed;
    bool jump = false;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if(isPlayer) { 
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //A = -1, D = 1;
            if(horizontalMove != 0)
                SocketClient.Send("</MOVE/>" + horizontalMove);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;


        /*if (timer - Time.fixedDeltaTime < -5)
        {
            runSpeed = 20;
        }
        else runSpeed = 40;*/

    }

    public void slowMovement()
    {
        timer = Time.fixedDeltaTime;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hit")
        {
            hp -= 40;
        }
    }*/
}
