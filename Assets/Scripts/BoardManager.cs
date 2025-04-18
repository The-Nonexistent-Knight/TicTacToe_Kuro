using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public Cell[,] grid = new Cell[3, 3];

    public GameObject cellPrefab;
    public Transform gridParent;
    private bool isInDestroyMode = false;
    public bool IsInDestroyMode => isInDestroyMode;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject obj = Instantiate(cellPrefab, gridParent);
                Cell cell = obj.GetComponent<Cell>();
                cell.x = i;
                cell.y = j;
            }
        }
    }

    public void RegisterCell(int x, int y, Cell cell)
    {
        grid[x, y] = cell;
    }

    public bool CheckVictory(Player player)
    {
        string symbol = player == Player.Human ? "X" : "O";

        for (int i = 0; i < 3; i++)
        {
            if (Match(symbol, grid[i, 0], grid[i, 1], grid[i, 2]) ||
                Match(symbol, grid[0, i], grid[1, i], grid[2, i]))
                return true;
        }

        if (Match(symbol, grid[0, 0], grid[1, 1], grid[2, 2]) ||
            Match(symbol, grid[0, 2], grid[1, 1], grid[2, 0]))
            return true;

        return false;
    }

    public bool IsFull()
    {
        foreach (var cell in grid)
            if (!cell.isOccupied) return false;
        return true;
    }

    private bool Match(string symbol, Cell a, Cell b, Cell c)
    {
        return a.symbol == symbol && b.symbol == symbol && c.symbol == symbol;
    }

    public void ResetBoard()
    {
        foreach (var cell in grid)
            cell.Reset();
    }

    public void EnterDestroyMode()
    {
        isInDestroyMode = true;
    }

    public void TryDestroyCell(Cell cell)
    {
        if (!isInDestroyMode) return;
        if (cell.symbol != "O") return;

        cell.Reset();
        isInDestroyMode = false;
    }

    public void UnlockAllLockedCells()
    {
        foreach (var cell in grid)
        {
            if (cell.isLocked && cell.lockForAIOnly)
            {
                cell.Unlock();
            }
        }
    }

}
