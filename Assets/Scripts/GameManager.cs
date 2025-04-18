using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { Human, AI }
public enum GameResult
{
    PlayerWin,
    AIWin,
    Draw
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player currentPlayer = Player.Human;
    public bool isGameOver = false;
    private Stack<MoveRecord> moveHistory = new Stack<MoveRecord>();
    public bool isDoubleMoveActive = false;
    public bool hasUsedFirstMove = false;
    public int totalWins = 0;



    private void Awake()
    {
        Instance = this;
    }

    public void SwitchTurn()
    {
        if (isGameOver) return;

        if (isDoubleMoveActive && !hasUsedFirstMove)
        {
            hasUsedFirstMove = true;
            Debug.Log("等待第二次落子");
            return;
        }
        isDoubleMoveActive = false;
        hasUsedFirstMove = false;
        currentPlayer = currentPlayer == Player.Human ? Player.AI : Player.Human;

        if (currentPlayer == Player.AI)
        {
            AIController.Instance.MakeMove();
        }
    }

    public void EndGame(GameResult result)
    {
        isGameOver = true;

        int energy = 0;
        string message = "";

        switch (result)
        {
            case GameResult.PlayerWin:
                energy = 5;
                message = "获胜!";
                totalWins++;
                break;

            case GameResult.AIWin:
                energy = 1;
                message = "落败……";
                break;

            case GameResult.Draw:
                energy = 2;
                message = "平局";
                break;
        }

        GameSession.Instance.AddEnergy(energy);
        UIController.Instance.ShowResult($"{message}\n增加{energy}能量");
    }


    public void RestartGame()
    {
        isGameOver = false;
        currentPlayer = Player.Human;
        BoardManager.Instance.ResetBoard();
    }

    public void RecordMove(int x, int y, string symbol, Player player)
    {
        moveHistory.Push(new MoveRecord(x, y, symbol, player));
        Debug.Log($"记录落子：{symbol} 在 ({x},{y}) by {player}");

    }

    public void RewindLastMove()
    {
        if (moveHistory.Count < 2)
        {
            Debug.LogWarning("回退步数不够.");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            MoveRecord record = moveHistory.Pop();
            Cell cell = BoardManager.Instance.grid[record.x, record.y];
            cell.Reset();
        }
        currentPlayer = Player.Human;
        isGameOver = false;
        UIController.Instance.HideResult();
    }

    public void StartDoubleMove()
    {
        isDoubleMoveActive = true;
        hasUsedFirstMove = false;
        Debug.Log("双重落子已激活！");
    }




}
