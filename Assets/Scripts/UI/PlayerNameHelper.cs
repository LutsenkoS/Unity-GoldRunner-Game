using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameHelper : MonoBehaviour {

    private Text nameText;
    private string currentName;
	void Start()
    {
        nameText = GetComponent<Text>();
        currentName = Settings.playerName;
        nameText.text = "Current player: " + currentName;       
    }

    void Update()
    {
       
        if (currentName != Settings.playerName)
        {
            currentName = Settings.playerName;
            nameText.text = "Current player: " + currentName;
            
        }
    }
}
