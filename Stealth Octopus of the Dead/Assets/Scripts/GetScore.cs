using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetScore : MonoBehaviour {
   GameObject Player;
   Backstab PlayerScore;
   public Text scoreText;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        scoreText.text = "0";
        PlayerScore = Player.GetComponent<Backstab>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = PlayerScore.killScore.ToString();
	}
}
