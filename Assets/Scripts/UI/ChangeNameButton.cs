using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameButton : MonoBehaviour {

    public Text playerName;
    void Start()
    {

    }
	public void ChangeName()
    {
        //playerName.GetComponentInChildren<Text>();
        if(playerName.text != string.Empty)
            Settings.playerName = playerName.text;
    }

}
