using UnityEngine;
using System.Collections;

public class GameEnder : MonoBehaviour {

    public int SceneNumber; //The scene the game will move to when this object is activated

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //if the player collides with the game ender object then the specified scene will be loaded. 
    void OnTriggerEnter(Collider col)
    {
      if (col.gameObject.tag == "Player")
        {
            //Save the time remaining to player prefs. 
            string levelnumber = "level" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString();
            TimerControl control = GameObject.FindGameObjectWithTag("TimerControl").GetComponent<TimerControl>();
            float score = control.timeOfGame - control.timeRemaining;
            PlayerPrefs.SetFloat(levelnumber, score);
            PlayerPrefs.Save();

            EndLevel();
        }
    }


    //Ends the level by loading the passed in scene
    public void EndLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNumber); 
    }

}
