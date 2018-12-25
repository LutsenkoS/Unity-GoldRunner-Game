using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.score += 1;
            if (GameManager.Instance.score > 20)
                GameManager.Instance.IncreaseEnemySpeed();
            Destroy(gameObject);
        }
    }
}
