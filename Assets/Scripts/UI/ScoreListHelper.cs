using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ScoreListHelper : MonoBehaviour
{
    Text scoreListText;
    string tempList;
    int maxNameLength = 25;
    List<string> playerScore;

    void Start()
    {
        scoreListText = GetComponent<Text>();
        LoadScoreFromXml();
        AddScoreToText();
    }

    void LoadScoreFromXml()
    {
        playerScore = new List<string>();
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode;
        try
        {
            xmlDoc.Load("Score.xml");
            rootNode = xmlDoc.ChildNodes[0];
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                playerScore.Add(node.InnerText);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }
    void AddScoreToText()
    {
        int divide = 0;
        foreach (var scoreElement in playerScore)
        {
            if (divide % 3 == 0)
            {
                tempList += "\n";
                tempList += scoreElement;
                tempList += "\n";
            }
            else
                tempList += SpacesFill(30) + scoreElement;
            divide++;
        }
        scoreListText.text = tempList;
    }

    private string SpacesFill(int count)
    {
        string spaces = string.Empty;
        for(int i = 0; i < count; i++)
        {
            spaces += " ";
        }
        return spaces;
    }
}
