using System;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform groundPlane;

    public int width = 10;
    public int height = 6;

    public float cellSize = 1;
    public GridCell[,] grid;
    private void Awake()
    {
        GenerateCell();
        FitGroundToGrid();

    }

    private void FitGroundToGrid()
    {
        if (groundPlane == null) return;

        float worldWidth = width * cellSize;
        float worldHeight = height * cellSize;

        groundPlane.position = new Vector3(
            (worldWidth - cellSize) / 2f,
            groundPlane.position.y,
            (worldHeight - cellSize) / 2f
        );

        groundPlane.localScale = new Vector3(
            worldWidth / 10f,
            1,
            worldHeight / 10f
        );
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
                cell.worldPosition = transform.position +
                     new Vector3(col * cellSize, 0, row * cellSize);
                grid[row, col] = cell;
            }
        }

        for (int row = 0; row < height; row++)
        {
            grid[row, width - 1].canBuild = true;
            grid[row, width - 1].isPath = true;
        }
    }
    void OnDrawGizmos()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Vector3 pos = transform.position +
              new Vector3(col * cellSize, 0, row * cellSize);

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
