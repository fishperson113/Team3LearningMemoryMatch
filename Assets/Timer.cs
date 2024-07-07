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
    [SerializeField] private float gameTime; // 60 seconds countdown timer

    public void CheckCountDown()
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
            yield return null;
        }
        Destroy(this.gameObject);
        GameOver();
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Optionally, reload the scene or show a game over screen
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
