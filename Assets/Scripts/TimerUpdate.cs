using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Transactions;
using UnityEngine.SceneManagement;

public class TimerUpdate : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    private bool timerStarted = false;
    public TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "00:00";
        StartCoroutine(StartTimer());
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted) {
            if(timeIsRunning)
            {
                if(timeRemaining >= 0)
                {
                    timeRemaining += Time.deltaTime;
                }
            }
            DisplayTime(timeRemaining);
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(seconds == 45f)
        {
            SceneManager.LoadScene("YouWon");
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3);
        timerStarted = true;
    }
}
