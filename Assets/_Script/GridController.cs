using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;

    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        UpdateGrid();
    }

    public void UpdateGrid()
    {
        if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            gridLayoutGroup.constraintCount = columns;
        }
        else if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            gridLayoutGroup.constraintCount = rows;
        }
        else
        {
            Debug.LogWarning("Please set the GridLayoutGroup constraint to either FixedColumnCount or FixedRowCount.");
        }
    }

    public void SetRows(int newRow)
    {
        rows = newRow;
        UpdateGrid();
    }

    public void SetColumns(int newColumn)
    {
        columns = newColumn;
        UpdateGrid();
    }
    public int GetRows()
    {
        return rows;
    }

    public int GetColumns()
    {
        return columns;
    }
}
