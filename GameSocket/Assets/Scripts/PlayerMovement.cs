using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;

    public float runSpeed = 40f;
    bool jump = false;
    public int health = 100;
    public GameObject deathEffect;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //A = -1, D = 1;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if(health <= 0)
        {
            Die();
        }

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        animator.SetBool("isDead", true);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        /*if (hp <= 0)
            animator.SetBool("isDead", true);*/

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hit")
        {
            hp -= 40;
        }
    }*/
}
