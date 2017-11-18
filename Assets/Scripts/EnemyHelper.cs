using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public enum Direction
{
    Top, Left, Right, Bottom
}
public class EnemyHelper : MonoBehaviour {

    public float speed;
    public float delta;
    //public GameObject wall;

    private bool attack;

    public bool InAttack
    {
        get { return attack; }
        set
        {
            attack = value;
            if (value == true) animator.SetBool("IsAttack", true);
        }
    }

    GameObject player;
    Animator animator;
    Rigidbody2D rb2D;
    int[,] path;
    BoardCreator board;
    Direction direction;
    float delay;
    float timeNow;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardCreator>();
        path = new int[board.width + 2, board.height + 2];
        path = board.path;
        direction = Direction.Top;
        delay = Time.realtimeSinceStartup;
        animator.SetBool("IsWalked", true);
    }

    
        
    
    void FixedUpdate()
    {
        if (InAttack)
            return;

        timeNow = Time.realtimeSinceStartup;
        if (timeNow > delay)
        {
            CheckDirection(false);            
        }
        Move(direction);
        
    }

    private List<Direction> CheckDirection(bool isBoard)
    {
        //list for possible directions to move
        List<Direction> directions = new List<Direction>();
        //save player position
        int x = Convert.ToInt32(transform.position.x);
        int y = Convert.ToInt32(transform.position.y);
        if (isBoard)
        {
            directions.AddRange(GetDirections(x, y));
        }
        else
        if (Mathf.Abs(x - transform.position.x) < delta && Mathf.Abs(y - transform.position.y) < delta)
        {
            directions.AddRange(GetDirections(x, y));
            if (directions.Count > 2 && !isBoard)
                ChangeDirection(directions);


        }
        return directions;

    }
    private List<Direction> GetDirections(int x, int y)
    {
        List<Direction> directions = new List<Direction>();

        if (x > 1)
            if (path[x - 1, y] == 0)
                directions.Add(Direction.Left);
        if (x < board.width)
            if (path[x + 1, y] == 0)
                directions.Add(Direction.Right);
        if (y > 1)
            if (path[x, y - 1] == 0)
                directions.Add(Direction.Bottom);
        if (y < board.height)
            if (path[x, y + 1] == 0)
                directions.Add(Direction.Top);
        return directions;
    }

    //private void Move(Direction dir)
    //{
    //    //bool top, right, bottom, left;
    //    //float xPos;
    //    //float yPos;
    //    //int x = Convert.ToInt32(transform.position.x);
    //    //int y = Convert.ToInt32(transform.position.y);
    //    //Direction direction;
    //    Vector3 movement = new Vector3();
    //    switch(dir)
    //    {
    //        case Direction.Top:
    //            {
    //                movement.Set(0.0f, 1.0f, 0.0f);
    //                break;
    //            }
    //        case Direction.Left:
    //            {
    //                movement.Set(-1.0f, 0.0f, 0.0f);
    //                break;
    //            }
    //        case Direction.Right:
    //            {
    //                movement.Set(1.0f, 0.0f, 0.0f);
    //                break;
    //            }
    //        case Direction.Bottom:
    //            {
    //                movement.Set(0.0f, -1.0f, 0.0f);
    //                break;
    //            }
    //    }
    //    //while ((transform.position.x - x) > 0.1 && (transform.position.y - y) > 0.1)
    //    //{            
    //    //    if (y < board.height - 1)
    //    //        if (path[x, y + 1] == 0)
    //    //        {
    //    //            movement.Set(0.0f, 1.0f, 0.0f);
    //    //        }
    //    //    if (x > 0)
    //    //        if (path[x - 1, y] == 0)
    //    //        {
    //    //            movement.Set(-1.0f, 0.0f, 0.0f);
    //    //        }
    //    //    if (x < board.width - 1)
    //    //        if (path[x + 1, y] == 0)
    //    //        {
    //    //            movement.Set(1.0f, 0.0f, 0.0f);
    //    //        }
    //    //    if (y > 0)
    //    //        if (path[x, y - 1] == 0)
    //    //        {
    //    //            movement.Set(0.0f, -1.0f, 0.0f);
    //    //        }
    //    //}
        
            
    //    movement = movement * speed * Time.deltaTime;
    //    transform.position += movement;

        
    //}
    private void Move(Direction dir)
    {       
        Vector2 movement = new Vector2();
        switch (dir)
        {
            case Direction.Top:
                {
                    movement.Set(0.0f, 1.0f);
                    break;
                }
            case Direction.Left:
                {
                    movement.Set(-1.0f, 0.0f);
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    break;
                }
            case Direction.Right:
                {
                    movement.Set(1.0f, 0.0f);
                    transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
                    break;
                }
            case Direction.Bottom:
                {
                    movement.Set(0.0f, -1.0f);
                    break;
                }
        }


        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
        //movement = movement * speed * Time.deltaTime;
        


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("Game over");            
        //    GameManager.Instance.GameOver = true;
        //}
        if (other.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("Touched the wall" + transform.position.x.ToString() + " " + transform.position.y.ToString());
            List<Direction> dirs = new List<Direction>();
            dirs.AddRange(CheckDirection(true));
            ChangeDirection(dirs);
            
            
            
            //Move(direction);
        }
        if (other.gameObject.CompareTag("Zombie") || other.gameObject.CompareTag("Mummy"))
        {
            ChangeDirection(new List<Direction>() { ReverseDirection(direction)});
        }
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack());
            //Attack();
            
            
            
            //Destroy(gameObject); 

            //GameOverText.gameObject.SetActive(true);
        }
    }

    private IEnumerator Attack()
    {
        if (player.transform.position.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else
            transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
        animator.SetBool("IsWalked", false);
        InAttack = true;
        
        yield return new WaitForSeconds(0.4f);
        Debug.Log("Game over");
        GameManager.Instance.GameOver = true;


        InAttack = false;
        
    }

    private void ChangeDirection(List<Direction> directions)
    {
        //Debug.Log(direction);
        Direction currentDirection = direction;
        if (directions.Count > 1)
        {
            directions.Remove(ReverseDirection(currentDirection));
        }
        //Debug.Log("Change direction" + directions.Count.ToString());
        //foreach (var dir in directions)
        //{
        //    Debug.Log(dir);
        //}

        
        direction = directions[new System.Random().Next(0, directions.Count)];
        //return to full number coordinates
        transform.position = new Vector3(Convert.ToInt32(transform.position.x), Convert.ToInt32(transform.position.y), 0.0f);
        //increase delay on 0.1 seconds for next time check direction
        delay = timeNow + 0.1f;
    }

    private Direction ReverseDirection(Direction currentDirection)
    {
        switch(currentDirection)
        {
            case Direction.Top:
                    return Direction.Bottom;
            case Direction.Bottom:
                    return Direction.Top;
            case Direction.Left:
                    return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return direction;
        }
    }
}
