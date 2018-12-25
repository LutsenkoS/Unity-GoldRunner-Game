using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed = 2f;
    public int TakedCoins
    {
        get
        {
            return takedCoins;
        }
    }
    private Vector2 _direction;
    private Vector3 _destination;
    Vector2 movement;
    Transform player;
    Animator animator;
    GameObject wall;
    int takedCoins;
    Rigidbody2D rb2D;
    bool isTurned;
    private BoardCreator board;
    void Start()
    {      
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        takedCoins = 0;
        _destination = transform.position;
        board = FindObjectOfType<BoardCreator>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (move.x != 0)
        {
            _direction.Set(move.x, 0f);
            if (move.x < 0 && !isTurned)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                isTurned = true;
            }
            else if (move.x > 0 && isTurned)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                isTurned = false;
            }
            
        }
        else if (move.y != 0)
        {
            _direction.Set(0f, move.y);
        }
        else if (move == Vector3.zero)
        {
            _direction = Vector2.zero;
        }

        if (_destination == transform.position)
        {
            _destination = transform.position + (Vector3)_direction;
        }
        if (_destination.x == 0 || _destination.y == 0 || _destination.x == board.width + 2 ||
            _destination.y == board.height + 2
            || board.path[(int) _destination.x, (int) _destination.y] == 1)
        {
            _destination = transform.position;
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);

        }
        
        bool walking = move.x != 0 || move.y != 0;
        animator.SetBool("isWalked", walking);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Move(h, v);
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

    }
    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.CompareTag("Mummy"))
                GameManager.Instance.score = 0;
  
       

    }
    
    
}
