using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject zombie;
    public GameObject mummy;
    public GameObject gameOverText;
    public GameObject hintToMainMenu;
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

    private int zombieCount;
    private bool isMummy = false;
    private bool gameOver;

    private Text playerNameText;
    private EnemyHelper zombie1;
    private EnemyHelper zombie2;
    private GameObject[] zombies;
    private EnemyHelper mummyEnemy;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {

        InstantiateEnemy(zombie);
        zombieCount = 1;
        Time.timeScale = 1;
        score = 0;
    }

    private void InstantiateEnemy(GameObject enemy)
    {
        board = GameObject.Find("BoardHolder").GetComponent<BoardCreator>();
        int startX = ((board.width % 2) == 0) ? (board.width - 1) : (board.width - 0);
        int startY = board.height / 3;
        Vector3 startPosition = new Vector3(startX, startY, 0.0f);
        Instantiate(enemy, startPosition, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (!GameOver)
                SaveScoreToXml();
            SceneManager.LoadScene(0);

        }
        CheckScore();

    }

    private void OnGameOver(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
            hintToMainMenu.SetActive(true);

            SaveScoreToXml();
        }
    }

    private void SaveScoreToXml()
    {
        Settings.pickedCoins = score;
        Settings.playTime = (float)Math.Round(Time.timeSinceLevelLoad, 2);

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode;
        try
        {
            xmlDoc.Load("Score.xml");
            rootNode = xmlDoc.ChildNodes[0];
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            rootNode = xmlDoc.CreateElement("Score");
            xmlDoc.AppendChild(rootNode);


        }

        XmlNode userNode;

        userNode = xmlDoc.CreateElement("PlayerName");
        userNode.InnerText = Settings.playerName;
        rootNode.AppendChild(userNode);
        //Debug.Log(Settings.playerName);
        userNode = xmlDoc.CreateElement("PickedCoins");
        userNode.InnerText = Settings.pickedCoins.ToString();
        rootNode.AppendChild(userNode);
        userNode = xmlDoc.CreateElement("PlayTime");
        userNode.InnerText = Settings.playTime.ToString();
        rootNode.AppendChild(userNode);

        xmlDoc.Save("Score.xml");
    }

    private void CheckScore()
    {

        if (score > 4 && zombieCount < 2)
        {
            InstantiateEnemy(zombie);
            zombieCount++;
            zombies = GameObject.FindGameObjectsWithTag("Zombie");
            zombie1 = zombies[0].GetComponent<EnemyHelper>();
            zombie2 = zombies[1].GetComponent<EnemyHelper>();
        }
        if (score > 9 && !isMummy)
        {
            InstantiateEnemy(mummy);
            isMummy = true;
            mummyEnemy = GameObject.FindGameObjectWithTag("Mummy").GetComponent<EnemyHelper>();
        }

    }



    public void IncreaseEnemySpeed()
    {
        zombie1.speed += zombie1.speed * 0.05f;
        zombie2.speed += zombie2.speed * 0.05f;
        mummyEnemy.speed += mummyEnemy.speed * 0.05f;
    }
}
