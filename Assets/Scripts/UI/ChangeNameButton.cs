using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameButton : MonoBehaviour {

    public Text playerName;

	public void ChangeName()
    {
        if(playerName.text != string.Empty)
            Settings.playerName = playerName.text;
    }

}
