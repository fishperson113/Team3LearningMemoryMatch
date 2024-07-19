using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameModeSelector : MonoBehaviour
{
    [SerializeField] private Button casualButton;
    [SerializeField] private Button customButton;
    [SerializeField] private TMP_InputField rowsInputField;
    [SerializeField] private TMP_InputField columnsInputField;

    private void Start()
    {
        casualButton.onClick.AddListener(SetCasualMode);
        customButton.onClick.AddListener(SetCustomMode);
    }

    private void SetCasualMode()
    {
        GameController.Instance.SetGameMode(GameMode.Casual, 2, 4);
        StartGame();
    }

    private void SetCustomMode()
    {
        int rows = int.Parse(rowsInputField.text);
        int columns = int.Parse(columnsInputField.text);
        GameController.Instance.SetGameMode(GameMode.Custom, rows, columns);
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
