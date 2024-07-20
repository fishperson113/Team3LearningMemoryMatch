using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameModeSelector : MonoBehaviour
{
    [SerializeField] private TMP_InputField rowsInputField;
    [SerializeField] private TMP_InputField columnsInputField;

    public void SetCasualMode()
    {
        GameController.Instance.SetGameMode(2, 4);
    }

    public void SetCustomMode()
    {
        int rows = int.Parse(rowsInputField.text);
        int columns = int.Parse(columnsInputField.text);
        GameController.Instance.SetGameMode( rows, columns);
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
