using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public GameObject coin;
    public int delay;
    public int maxCoins;

    int coins;
    int width;
    int height;
    BoardCreator board;
    float lastInterval;
    float timeNow;

    void Start()
    {
        board = GameObject.Find("GameManager").GetComponent<BoardCreator>();
        width = board.width;
        height = board.height;
        lastInterval = Time.realtimeSinceStartup;
        
    }

    void Update()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin").Length;
        timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + delay)
        {
            InstantiateCoin();
            lastInterval = timeNow;
        }
               
    }

    private void InstantiateCoin()
    {
        
        if (coins < maxCoins)
        {
            float x = UnityEngine.Random.Range(1, width);
            x = (x == 1) ? 1 : ((x % 2) == 1) ? x : x - 1;
            float y = UnityEngine.Random.Range(1, height);
            Vector3 position = new Vector3(x, y, 0.0f);            

            Instantiate(coin, position, Quaternion.identity);            
        }
    }

   
}
