using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimePassed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timePassed;

    public void UpdateTimePassed(int time)
    {
        timePassed.text = "Time: " + time.ToString() + 's';
    }

}
