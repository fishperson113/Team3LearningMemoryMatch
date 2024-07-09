using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
