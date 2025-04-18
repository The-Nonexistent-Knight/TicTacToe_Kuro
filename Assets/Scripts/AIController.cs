using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController Instance;

    private IAIStrategy easyAI = new EasyAIStrategy();
    private IAIStrategy brutalAI = new BrutalAIStrategy();
    private IAIStrategy currentStrategy;
    private bool isSealed = false;


    private void Awake()
    {
        Instance = this;
        currentStrategy = easyAI;
    }

    public void SetDifficulty(bool useBrutal)
    {
        currentStrategy = useBrutal ? brutalAI : easyAI;
    }

    public void MakeMove()
    {
        var grid = BoardManager.Instance.grid;

        (int x, int y) move;

        if (isSealed)
        {
            isSealed = false;
            Debug.Log("AI 使用随机策略落子！");
            move = GetRandomMove(grid);
        }
        else
        {
            move = currentStrategy.GetMove(grid);
        }

        Cell cell = grid[move.x, move.y];
        if (cell.isLocked)
        {
            Debug.Log("AI落在锁定格子");
        }
        cell.SetSymbol("O");

        if (BoardManager.Instance.CheckVictory(Player.AI))
        {
            GameManager.Instance.EndGame(GameResult.AIWin);
        }
        else if (BoardManager.Instance.IsFull())
        {
            GameManager.Instance.EndGame(GameResult.Draw);
        }
        else
        {
            GameManager.Instance.currentPlayer = Player.Human;
        }
        BoardManager.Instance.UnlockAllLockedCells();
    }

    public void SealNextMove()
    {
        isSealed = true;
        Debug.Log("AI 下一步将随机落子");
    }

    private (int x, int y) GetRandomMove(Cell[,] grid)
    {
        List<(int x, int y)> available = new List<(int, int)>();

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (!grid[i, j].isOccupied)
                    available.Add((i, j));

        return available[Random.Range(0, available.Count)];
    }


}

