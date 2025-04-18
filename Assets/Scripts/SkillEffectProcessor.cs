using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillEffectProcessor
{
    public static void ApplyEffect(SkillEffectType effect)
    {
        switch (effect)
        {
            case SkillEffectType.Rewind:
                Debug.Log("悔棋触发！");
                GameManager.Instance.RewindLastMove();
                break;
            case SkillEffectType.SealAI:
                Debug.Log("智能封印触发！");
                AIController.Instance.SealNextMove();
                break;
            case SkillEffectType.DestroyPiece:
                Debug.Log("摧毁棋子触发！");
                BoardManager.Instance.EnterDestroyMode();
                break;
            case SkillEffectType.DoubleMove:
                Debug.Log("双重落子触发！");
                GameManager.Instance.StartDoubleMove();
                break;
            case SkillEffectType.ReplaceCard:
                Debug.Log("卡牌替换触发！");
                SkillDeckManager.Instance.ReplaceAllSkillCards();
                break;
            case SkillEffectType.LockCell:
                Debug.Log("封锁格子触发！");
                LockRandomCellForAI();
                break;
        }
    }
    private static void LockRandomCellForAI()
    {
        var grid = BoardManager.Instance.grid;
        List<Cell> available = new List<Cell>();

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                Cell cell = grid[i, j];
                if (!cell.isOccupied && !cell.isLocked)
                {
                    available.Add(cell);
                }
            }

        if (available.Count == 0)
        {
            Debug.Log("无可封锁的格子");
            return;
        }

        int index = Random.Range(0, available.Count);
        available[index].LockForAI();

        Debug.Log($"封锁了格子：({available[index].x}, {available[index].y})");
    }

}
