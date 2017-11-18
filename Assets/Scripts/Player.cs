using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //public Vector2 velocity;
    public float speed = 2f;
    public int TakedCoins
    {
        get
        {
            return takedCoins;
        }
    }

    Vector2 movement;
    Transform player;
    Animator animator;
    GameObject wall;
    int takedCoins;
    Rigidbody2D rb2D;
    bool isTurned;
    //public static Player Instance
    //{
    //    get
    //    {
    //        return instance;
    //    }
    //}
    //private static Player instance;

    void Start()
    {
        //if (instance)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //instance = this;        
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        takedCoins = 0;
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
    }

    private void Move(float h, float v)
    {
        if (h != 0)
            movement = new Vector2(h, 0.0f);
        if (v != 0)
            movement = new Vector2(0.0f, v);
        if (h < 0 && !isTurned)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
            isTurned = true;
        }
        if (h > 0 && isTurned)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            isTurned = false;
        }
        //movement = movement * speed * Time.deltaTime;
        //transform.position += movement;
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
        //walking animation
        bool walking = h != 0 || v != 0;
        animator.SetBool("isWalked", walking);

        //Debug.Log(h);    
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //if(other.gameObject.CompareTag("Zombie") || other.gameObject.CompareTag("Mummy"))
        //{
            if (other.gameObject.CompareTag("Mummy"))
                GameManager.Instance.score = 0;
            //GameManager.Instance.GameOver = true;
            //Destroy(gameObject);
        //}
       

    }
    
    
}
