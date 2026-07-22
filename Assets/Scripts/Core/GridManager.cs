using System;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 6;

    public float cellSize = 1;
    public GridCell[,] grid;
    private void Awake()
    {
        GenerateCell();
    }

    private void GenerateCell()
    {
        grid = new GridCell[height, width];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                GridCell cell = new GridCell();
                cell.col = col;
                cell.row = row;

                grid[row,col] = cell;
            }
        }

        for (int row = 0; row < height; row++)
        {
            grid[row,width-1].isPath = true; 
        }

        for (int row = 0;row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (col == width - 1) continue;
                grid[row,col].canBuild = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Vector3 pos = new Vector3(col * cellSize, 0, row * cellSize);

                if (grid != null)
                {
                    GridCell cell = grid[row, col];
                    if (cell.isPath) Gizmos.color = Color.red;
                    else if (cell.canBuild) Gizmos.color = Color.green;
                    else Gizmos.color = Color.gray;
                }
                else
                {
                    Gizmos.color = Color.white; 
                }

                Gizmos.DrawWireCube(pos, Vector3.one * cellSize);
            }
        }
    }
}
