using UnityEngine;
using System.Collections.Generic;

public class FlowField : MonoBehaviour
{
    public Cell[,] grid { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public float cellRadious { get; private set; }
    private float cellDiameter;
    public Transform spawnPoint;
    public FlowField(float _cellRadious,Vector2Int _gridSize)
    {
        cellRadious = _cellRadious;
        cellDiameter = cellRadious * 2;
        gridSize = _gridSize;
    }

    public void CreateGrid()
    {
        grid = new Cell[gridSize.x, gridSize.y];
        for (int x = 0;  x< gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 worlPosition = new Vector3(spawnPoint.position.x, 0, spawnPoint.position.y);
                grid[x, y] = new Cell(worlPosition, new Vector2Int(x, y));
            }
        }
    }

    public void CreateCostField()
    {
        Vector3 cellHalfExtents = Vector3.one * cellRadious;
        int terrainMask = LayerMask.GetMask("Impassible", "RoughTerrain");
        foreach (Cell curCell in grid)
        {
            Collider[] obstacles = Physics.OverlapBox(curCell.worldPosation, cellHalfExtents, Quaternion.identity, terrainMask);
            bool hasIncreasedCost = false;
            foreach (Collider col in obstacles)
            {
                if (col.gameObject.layer == 8)
                {
                    curCell.IncreaseTheCaust(255);
                    continue;
                }
                else if (!hasIncreasedCost && col.gameObject.layer == 9)
                {
                    curCell.IncreaseTheCaust(3);
                    hasIncreasedCost = true;
                }
            }
        }
    }
}
