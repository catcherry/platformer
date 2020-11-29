using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : BaseHero
{
    private SpriteRenderer img;
    private Rigidbody2D rb;
    private bool goToLeft = false;
    private float time;
    private float period = 2f;
    public Vector2 move;
    private Animator animator;
    private GameObject playerNear;
    private bool falling = false;

    void Start()
    {
        hp = 100;
        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        img.enabled = !isBlinking;
        isBlinking = false;
        playerNear = CheckCloseToTag("Player", 7);
        if (playerNear != null && playerNear.GetComponent<PlayerScript>().IsInputIgnored()) playerNear = null;

        if (!falling && (Time.time > time))
        {
            goToLeft = !goToLeft;
            time = Time.time + period;
        }


        move.y = rb.velocity.y;
        if (falling)
        {
            move.y -= 10;
        }
        move.x = goToLeft ? -speed : speed;
        if (!falling)
            img.flipX = (playerNear == null) ? goToLeft : !playerNear.GetComponent<SpriteRenderer>().flipX;



        if (falling || playerNear == null)
            rb.velocity = move;

        animator.SetBool("Walk", playerNear == null);
        animator.SetBool("Stab", playerNear != null);

        if (playerNear != null)
        {
            playerNear.GetComponent<PlayerScript>().RemoveHealth();
        }

    }



    public override void Die()
    {
        base.Die();
    }


    public override void RemoveHealth()
    {
        base.RemoveHealth();
        if (hp <= 0)
        {
            falling = true;
            IgnoreGroundCollision();
            Invoke("Die", 1f);
        }
    }


}
