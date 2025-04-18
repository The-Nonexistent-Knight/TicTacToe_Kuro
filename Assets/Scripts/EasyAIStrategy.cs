using System.Collections.Generic;
using UnityEngine;


public class EasyAIStrategy : IAIStrategy
{
    public (int x, int y) GetMove(Cell[,] grid)
    {
        Debug.Log("简单AI Strategy 运行中");
        if (Random.value < 1f)
        {
            var move = TryGreedyMove(grid);
            if (move.HasValue) return move.Value;
        }

        List<(int x, int y)> available = new List<(int, int)>();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (!grid[i, j].isOccupied && !grid[i, j].isLocked)
                    available.Add((i, j));

        return available[Random.Range(0, available.Count)];
    }

    private (int, int)? TryGreedyMove(Cell[,] grid)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (!grid[i, j].isOccupied)
                {
                    grid[i, j].SetSymbolSimulate("O");
                    if (BoardManager.Instance.CheckVictory(Player.AI))
                    {
                        grid[i, j].Reset();
                        return (i, j);
                    }
                    grid[i, j].Reset();
                }
            }

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (!grid[i, j].isOccupied && !grid[i, j].isLocked)
                {
                    grid[i, j].SetSymbolSimulate("X");
                    if (BoardManager.Instance.CheckVictory(Player.Human))
                    {
                        grid[i, j].Reset();
                        return (i, j);
                    }
                    grid[i, j].Reset();
                }
            }

        return null;
    }

}
