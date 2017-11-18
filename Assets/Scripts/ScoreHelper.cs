using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHelper : MonoBehaviour {

    Text score;
    void Start()
    {
        score = GetComponent<Text>();
    }

	void Update()
    {
        score.text = "Coins: " + GameManager.Instance.score.ToString(); 
    }
}
