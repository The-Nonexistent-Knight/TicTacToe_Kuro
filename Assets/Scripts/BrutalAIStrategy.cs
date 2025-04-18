using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutalAIStrategy : IAIStrategy
{
    public (int x, int y) GetMove(Cell[,] grid)
    {
        Debug.Log("¿‰ø·AI Strategy ‘À––÷–");
        int bestScore = int.MinValue;
        (int x, int y) bestMove = (-1, -1);

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (!grid[i, j].isOccupied && !grid[i, j].isLocked)
                {
                    grid[i, j].SetSymbolSimulate("O");
                    int score = Minimax(grid, false, 0, int.MinValue, int.MaxValue);
                    grid[i, j].Reset();

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (i, j);
                    }
                }
            }

        return bestMove;
    }

    private int Minimax(Cell[,] grid, bool isMaximizing, int depth, int alpha, int beta)
    {
        if (BoardManager.Instance.CheckVictory(Player.AI)) return 10 - depth;
        if (BoardManager.Instance.CheckVictory(Player.Human)) return depth - 10;
        if (BoardManager.Instance.IsFull()) return 0;

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (!grid[i, j].isOccupied && !(grid[i, j].isLocked && grid[i, j].lockForAIOnly && isMaximizing))
                {
                    grid[i, j].SetSymbolSimulate(isMaximizing ? "O" : "X");
                    int score = Minimax(grid, !isMaximizing, depth + 1, alpha, beta);
                    grid[i, j].Reset();

                    if (isMaximizing)
                    {
                        bestScore = Mathf.Max(bestScore, score);
                        alpha = Mathf.Max(alpha, score);
                    }
                    else
                    {
                        bestScore = Mathf.Min(bestScore, score);
                        beta = Mathf.Min(beta, score);
                    }

                    if (beta <= alpha) break;
                }
            }

        return bestScore;
    }

}
