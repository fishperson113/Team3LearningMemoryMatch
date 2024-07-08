using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    void Start()
    {
        score = 0; 
    }

    public void AddScore(int points)
    {
        score += points;
    }
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

     void Update()
    {
        UpdateScoreText(); 
    }
}
