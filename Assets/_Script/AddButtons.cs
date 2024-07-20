using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private int puzzleAmount;
    [SerializeField]
    private GameObject btn;

    public void CreatePuzzle()
    {
        GridController gridController = puzzleField.GetComponent<GridController>();
        if (gridController != null)
        {
            int rows = gridController.GetRows();
            int columns = gridController.GetColumns();

            puzzleAmount = rows * columns;
            if(puzzleAmount % 2 != 0)
            {
                puzzleAmount++;
            }
        }

        for (int i = 0; i < puzzleAmount; i++)
        {
            GameObject button = Instantiate(btn, puzzleField);
            button.name = "" + i;
        }
    }
}
