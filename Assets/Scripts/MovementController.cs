using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float speed;

    private Vector2 _direction;
    private Vector3 _destination;
    private Coroutine coroutine;
    private Rigidbody2D rb2D;
    void Start()
    {
        _destination = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update ()
	{
	    Move();
	}

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (move.x != 0)
        {
            _direction.Set(move.x, 0f);
        }
        else if (move.y != 0)
        {
            _direction.Set(0f, move.y);
        }
        else if(move == Vector3.zero)
        {
            _direction = Vector2.zero;
        }
            
        
        if (_destination == transform.position)
        {
            _destination = transform.position + (Vector3)_direction;
        }
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        Debug.Log(_destination);
        rb2D.MovePosition(_destination * speed * Time.deltaTime);
        yield return null;
    }
}
