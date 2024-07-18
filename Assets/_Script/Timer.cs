using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Timer variables
    public TextMeshProUGUI timerText;
    [SerializeField] private float gameTime;
    private int TimePassed;
    private bool isGameOver = false;

    public void CountDown()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60F);
            int seconds = Mathf.FloorToInt(gameTime % 60F);
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            TimePassed = 60 - seconds;
            yield return null;
        }
        isGameOver = true;
        Destroy(this.gameObject);
    }
    public int getTimePassed()
    {
        return TimePassed;
    }    

    public bool checkGameOver()
    {
        return isGameOver == true;
    }
}
