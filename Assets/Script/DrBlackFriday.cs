﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrBlackFriday : Enemy
{ 
    private Rigidbody2D r2D; 
    private Animator anim;

    public void Start()
    { 
        r2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    public override void FixedUpdate()
    {
        Vector3 temp = transform.localScale;
        speed = 0; 
        anim.SetBool("Walk", false); 
        distance = Vector2.Distance(transform.position, player.position);

        // cho quai thuc day khi player nam trong khoang distanceMIN va ngu lai khi player ra khoi
        if (player.transform.position.x > this.transform.position.x && distance < distanceMIN)
        {
            speed = 1f;
            anim.SetBool("Walk", true);
            temp.x = 155f;
            transform.localScale = temp; 
            if (distance <= 120)
            {
                anim.SetBool("Attack", true);
                speed = 0;
            }
            else
            {
                anim.SetBool("Attack", false);
                speed = 1f;
            }
        }
        else

        if (player.transform.position.x < this.transform.position.x && distance < distanceMIN)
        {
            speed = 1f;
            r2D.AddForce(Vector2.left * speed);
            anim.SetBool("Walk", true);
            temp.x = -155f;
            transform.localScale = temp;
            if (distance < 100)
            {
                anim.SetBool("Attack", true);
                speed = 0;

            }
            else
            {
                anim.SetBool("Attack", false);
                speed = 1f;

            }
        }
        colliderPlayer = Physics2D.Linecast(pointA.position, pointB.position, 1 << 8);
        Debug.DrawLine(pointA.position, pointB.position, Color.green);
        if (!colliderPlayer)
        {
            speed = 0;
            anim.SetBool("Walk", false);
        }
        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(gameObject);
        }
        r2D.velocity = new Vector2(transform.localScale.x, 0) * speed;
        Check_Sword_Arc();

    }

    //kiem tra xem player co chem trung khong
    void Check_Sword_Arc()
    {
        float checkSword = Vector2.Distance(Sword_Arc.position, transform.position); 
        if (checkSword < 150 && Input.GetKeyDown(KeyCode.Space))
        {
            health -= 10;
            anim.SetTrigger("Hit");
            r2D.AddForce(Vector2.right * speed * 0);
        }
    }
    void Update()
    {
        
    }
}
