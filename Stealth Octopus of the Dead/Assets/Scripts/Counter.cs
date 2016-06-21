using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{

    public Text minuteText;
    public Text secondText;
    public float timeOfGame = 300.0f;
    bool stopTime = false;
    string currentScene;

    // Update is called once per frame

    void Update()
    {
        if (stopTime == false)
        {
            timeOfGame -= Time.deltaTime;

            float minutes = Mathf.Floor(timeOfGame / 60);
            float seconds = timeOfGame % 60;

            if (minutes < 0 && seconds <= 0)
            {
                stopTime = true;
                minutes = 0;
                seconds = 0;
                SceneManager.LoadScene(0);
            }
            secondText.text = minuteText.text +"" + seconds.ToString("00");
        }
    }
}
