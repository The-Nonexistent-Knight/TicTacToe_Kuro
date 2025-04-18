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
                Debug.Log("���崥����");
                GameManager.Instance.RewindLastMove();
                break;
            case SkillEffectType.SealAI:
                Debug.Log("���ܷ�ӡ������");
                AIController.Instance.SealNextMove();
                break;
            case SkillEffectType.DestroyPiece:
                Debug.Log("�ݻ����Ӵ�����");
                BoardManager.Instance.EnterDestroyMode();
                break;
            case SkillEffectType.DoubleMove:
                Debug.Log("˫�����Ӵ�����");
                GameManager.Instance.StartDoubleMove();
                break;
            case SkillEffectType.ReplaceCard:
                Debug.Log("�����滻������");
                SkillDeckManager.Instance.ReplaceAllSkillCards();
                break;
            case SkillEffectType.LockCell:
                Debug.Log("�������Ӵ�����");
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
            Debug.Log("�޿ɷ����ĸ���");
            return;
        }

        int index = Random.Range(0, available.Count);
        available[index].LockForAI();

        Debug.Log($"�����˸��ӣ�({available[index].x}, {available[index].y})");
    }

}
