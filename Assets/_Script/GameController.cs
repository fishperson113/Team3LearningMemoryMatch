using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : Singleton<GameController>
{
    [SerializeField] private Sprite bgImage;
    private Sprite[] puzzles;
    private List<Sprite> gamePuzzles = new List<Sprite>();
    private List<Button> btns = new List<Button>();

    private int currentScore;
    private bool firstGuess, secondGuess;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    [SerializeField] private GameObject timerPrefab;
    private Timer timer;
    [SerializeField] private GameObject scorePrefab;
    private ScoreSystem scoreSystem;

    [SerializeField] private GameObject scoreAchievedPrefab;
    private ScoreAchieved scoreAchieved;

    [SerializeField] private GameObject GameOverScoreAchievedPrefab;
    private ScoreAchieved GameOverScoreAchieved;

    [SerializeField] private GameObject timePassedPrefab;
    private TimePassed TimeHasPassed;

    [SerializeField] private GameObject winAnnouncement;

    [SerializeField] private GameObject GameOverScreen;

    private AddButtons addButtons;
    private int rows=2;
    private int columns=4;

    [SerializeField] private GridController gridController;
    protected override void Awake()
    {
        base.Awake();
        addButtons = GetComponent<AddButtons>();
        puzzles = Resources.LoadAll<Sprite>("Sprites");
    }
    public void Init()
    {
        timer = timerPrefab.GetComponent<Timer>();
        scoreSystem = scorePrefab.GetComponent<ScoreSystem>();
        scoreAchieved = scoreAchievedPrefab.GetComponent<ScoreAchieved>();
        TimeHasPassed = timePassedPrefab.GetComponent<TimePassed>();
        GameOverScoreAchieved = GameOverScoreAchievedPrefab.GetComponent<ScoreAchieved>();
        ApplyGameSettings();
    }

    public void StartGame()
    {
        currentScore = 0;
        UpdateScoreText();

        gridController.UpdateGrid();
        GetButton();
        AddListeners();
        AddGamePuzzles();

        Utility.Shuffle(gamePuzzles);
        timer.CountDown();
        winAnnouncement.SetActive(false);
        GameOverScreen.SetActive(false);
        StartCoroutine(CheckTimeOverRoutine());
    }

    public void SetGameMode(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }

    private void ApplyGameSettings()
    {
        if (gridController != null)
        {
            if (rows > 0 && columns > 0)
            {
                gridController.SetRows(rows);
                gridController.SetColumns(columns);
            }
            else
            {
                Debug.LogError("Rows and columns must be greater than 0 for Custom mode.");
            }
        }
    }
    void GetButton()
    {
        addButtons.CreatePuzzle();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
            btns[i].name = i.ToString();  // Ensure buttons have unique names
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    void PickAPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            StartCoroutine(CardRotator.RotateCard(btns[firstGuessIndex], gamePuzzles[firstGuessIndex]));
        }
        else if (!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            StartCoroutine(CardRotator.RotateCard(btns[secondGuessIndex], gamePuzzles[secondGuessIndex]));

            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            AddScore(10);

            CheckIfTheGameisFinished();
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            StartCoroutine(CardRotator.RotateCard(btns[firstGuessIndex], bgImage));
            StartCoroutine(CardRotator.RotateCard(btns[secondGuessIndex], bgImage));
        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;
    }

    void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreSystem.UpdateScoreText(currentScore);
    }

    void CheckIfTheGameisFinished()
    {
        foreach (Button btn in btns)
        {
            if (btn.interactable)
            {
                return;
            }
        }
        Debug.Log("Game Finished!");
        winAnnouncement.SetActive(true);
        WinScreen();
    }

    void WinScreen()
    {
        Time.timeScale = 0f;
        scoreAchieved.ScoreWasAchieved(currentScore);
        TimeHasPassed.UpdateTimePassed(timer.getTimePassed());
    }

    IEnumerator CheckTimeOverRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (timer.checkGameOver())
            {
                if (GameOverScreen != null)
                {
                    GameOverScreen.SetActive(true);
                    GameOverScoreAchieved.ScoreWasAchieved(currentScore);
                }
                yield break;  
            }
        }
    }
}
