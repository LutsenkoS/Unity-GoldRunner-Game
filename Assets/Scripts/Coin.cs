using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    
    //GameObject player;
    //Collider2D playerCollider;
    void Start()
    {        
        //player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        //playerCollider = player.GetComponent<Collider2D>();
    }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("enter trigger");
        if (other.gameObject.CompareTag("Player"))// == player.GetComponent<Collider2D>())
        {
            //Debug.Log("player trigger");
            GameManager.Instance.score += 1;
            if (GameManager.Instance.score > 20)
                GameManager.Instance.IncreaseEnemySpeed();
            Destroy(gameObject);
        }
    }
}
