using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    [SerializeField] private GameObject timerPrefab;
    private Timer timer;


    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites");
        timer = timerPrefab.GetComponent<Timer>();
   
    }

 
    void Start()
    {
        GetButton();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        timer.CheckCountDown();
    }

   

    void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
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

    public void PickAPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            StartCoroutine(RotateCard(btns[firstGuessIndex], gamePuzzles[firstGuessIndex]));
        }
        else if (!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            StartCoroutine(RotateCard(btns[secondGuessIndex], gamePuzzles[secondGuessIndex]));

            countGuesses++;

            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator RotateCard(Button btn, Sprite newImage)
    {
        // Rotate the card to 90 degrees over 0.25 seconds
        for (float i = 0; i <= 0.25f; i += Time.deltaTime)
        {
            btn.transform.rotation = Quaternion.Euler(0, 180 * i / 0.25f, 0);
            yield return null;
        }
        btn.image.sprite = newImage;

        // Complete the rotation to 180 degrees over 0.25 seconds
        for (float i = 0; i <= 0.25f; i += Time.deltaTime)
        {
            btn.transform.rotation = Quaternion.Euler(0, 180 + 180 * i / 0.25f, 0);
            yield return null;
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

            CheckIfTheGameisFinished();
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            StartCoroutine(RotateCard(btns[firstGuessIndex], bgImage));
            StartCoroutine(RotateCard(btns[secondGuessIndex], bgImage));
        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameisFinished()
    {
        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished!");
            Debug.Log("It took you " + countGuesses + " many guess(es) to finish the game");
        }
    }

    void Shuffle(List<Sprite> List)
    {
        for (int i = 0; i < List.Count; i++)
        {
            Sprite temp = List[i];
            int randomIndex = Random.Range(i, List.Count);
            List[i] = List[randomIndex];
            List[randomIndex] = temp;
        }
    }

}
