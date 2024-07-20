using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;
    private int rows;
    private int columns;
    

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
