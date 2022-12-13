using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    string isPlayer;

    public float runSpeed;
    public bool jump = false;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == isPlayer)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //A = -1, D = 1;
            if (horizontalMove != 0)
            {
                animator.SetBool("IsRunning", true);
            }
            else
                animator.SetBool("IsRunning", false);

            if (Input.GetButtonDown("Jump"))
            {
                SocketClient.Send("</JUMP/>");
                JumpAnimation();
            }
        }
    }

    private void Awake()
    {
        isPlayer = PlayerPrefs.GetString("color");
    }

    public void OnLanding()
    {
        Debug.Log("SONO A TERRA");
        animator.SetBool("isJumping", false);
    }

    public void JumpAnimation()
    {
        jump = true;
        animator.SetBool("isJumping", true);
    }

    private void FixedUpdate()
    {
        if (isPlayer == this.gameObject.name)
        {
            SocketClient.Send("</MOVE/>" + this.transform.position.x + ";" + this.transform.position.y);
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        }
        jump = false;
        /*if (timer - Time.fixedDeltaTime < -5)
        {
            runSpeed = 20;
        }
        else runSpeed = 40;*/

    }
    public void slowMovement()
    {
        //timer = Time.fixedDeltaTime;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SocketClient.Send("")
        }
    }*/
}
