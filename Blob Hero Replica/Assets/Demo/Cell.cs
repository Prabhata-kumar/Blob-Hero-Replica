using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 worldPosation;
    public Vector2Int gridIndex;
    public byte cost;
    public Cell(Vector3 _worldPosition, Vector2Int _gridIndex)
    {
        worldPosation = _worldPosition;
        gridIndex = _gridIndex;
        cost = 1;
    }

    public void IncreaseTheCaust(int amt)
    {
        if (cost == byte.MaxValue) return;
        if(amt +cost >=255)
        {
            cost = byte.MaxValue;
        }
        else
        {
            cost += (byte)amt;
        }
    }
}
