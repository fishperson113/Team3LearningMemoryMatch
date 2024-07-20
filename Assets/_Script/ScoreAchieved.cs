using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAchieved : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score_Achieved;

    public void ScoreWasAchieved(int score)
    {
        Score_Achieved.text = "Score: " + score.ToString();
    }
}
