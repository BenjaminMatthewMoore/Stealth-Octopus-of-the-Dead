using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class ScoreLoader : MonoBehaviour {

    private float score;
    private int recentLevel;
    public Text text; 
	// Use this for initialization
	void Start () {
        //load in our player prefs
        recentLevel = PlayerPrefs.GetInt("recent");
        score = PlayerPrefs.GetFloat("level" + recentLevel.ToString());
        //now adjust the score to a time format
        float minutes = Mathf.Floor(score / 60);
        float seconds = score % 60;

        text.text = minutes.ToString() + ": " + seconds.ToString("00");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
