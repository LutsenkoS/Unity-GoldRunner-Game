using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject zombie;
    public GameObject mummy;
    public GameObject gameOverText;
    public int score;
    

    public string PlayerName
    {
        get { return playerName; }
        set
        {            
            playerName = value;
            playerNameText = GameObject.Find("PlayerName").GetComponent<Text>();
            playerNameText.text = "Current player: " + value;
        }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
            OnGameOver(value);
            
        }
    }
    

    //private GameOverHelper GameOverText;
    BoardCreator board;
    int[,] path;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameManager instance = null;
    private string playerName;

    //private bool isZombie = false;
    private int zombieCount;    
    private bool isMummy = false;
    private bool gameOver;

    private Text playerNameText;
    private EnemyHelper zombie1;
    private EnemyHelper zombie2;
    private GameObject[] zombies;
    private EnemyHelper mummyEnemy;
    //private int scoreLimit = 0;
    //private int zombieLimit = 1;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);

        //GameOverText = FindObjectOfType<GameOverHelper>();

        //score = GetComponent<Text>();
    }
    void Start()
    {      
        
        //Instantiate(player, new Vector3(1.0f, 1.0f, 0.0f), Quaternion.identity);
        InstantiateEnemy(zombie);
        zombieCount = 1;
        Time.timeScale = 1;
        score = 0;
    }

    private void InstantiateEnemy(GameObject enemy)
    {
        board = GetComponent<BoardCreator>();       
        int startX = ((board.width % 2) == 0) ? (board.width - 1) : (board.width - 0);
        int startY = board.height / 3;
        Vector3 startPosition = new Vector3(startX, startY, 0.0f);
        Instantiate(enemy, startPosition, Quaternion.identity);
    }

    void Update()
    {
        if(Input.GetKey("escape"))
        {
            
            SceneManager.LoadScene(0);   
        }
        //score.text = "Coins: " + player.TakedCoins.ToString();
        CheckScore();
        
    }
    //private void OnGameOver()
    //{
    //    Time.timeScale = 0;
    //    GameOverText.gameObject.SetActive(true);
    //}
    private void OnGameOver(bool value)
    {
        if (value)
        {

            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }
    private void CheckScore()
    {
        
        if (score > 4 && zombieCount < 2) 
        {
            InstantiateEnemy(zombie);
            //scoreLimit += 5;
            zombieCount++;
            //if(zombieLimit < 2)
            //    zombieLimit += 1;
            zombies = GameObject.FindGameObjectsWithTag("Zombie");
            zombie1 = zombies[0].GetComponent<EnemyHelper>();
            //if(zombieCount > 1)
                zombie2 = zombies[1].GetComponent<EnemyHelper>();
        }
        if(score > 9 && !isMummy)
        {
            InstantiateEnemy(mummy);
            isMummy = true;
            mummyEnemy = GameObject.FindGameObjectWithTag("Mummy").GetComponent<EnemyHelper>();
        }
       
    }

    

    public void IncreaseEnemySpeed()
    {
        //if (zombie1.speed < 2f)
        //{
            zombie1.speed += zombie1.speed * 0.05f;
            zombie2.speed += zombie2.speed * 0.05f;
        //}
        mummyEnemy.speed += mummyEnemy.speed * 0.05f;
    }
}
