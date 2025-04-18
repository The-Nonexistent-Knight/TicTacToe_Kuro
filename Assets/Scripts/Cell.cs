using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int x, y;

    [Header("UI References")]
    public TextMeshProUGUI symbolText;
    public Image symbolImage;

    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite hoverSprite;
    public Sprite playerSprite;
    public Sprite aiSprite;
    public Sprite lockedSprite;


    public bool isOccupied = false;
    public string symbol = "";
    public bool isLocked = false;
    public bool lockForAIOnly = false;

    private void Awake()
    {
        if (symbolText == null)
            symbolText = GetComponentInChildren<TextMeshProUGUI>();

        if (symbolImage == null)
            Debug.LogError("symbolImage Î´°ó¶¨");

        StartCoroutine(DelayedRegister());
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isOccupied && !isLocked && symbolImage != null)
        {
            symbolImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isOccupied && !isLocked && symbolImage != null)
        {
            symbolImage.sprite = emptySprite;
        }
    }

    IEnumerator DelayedRegister()
    {
        yield return null;
        BoardManager.Instance.RegisterCell(x, y, this);
    }

    public void OnClick()
    {
        if (BoardManager.Instance.IsInDestroyMode)
        {
            BoardManager.Instance.TryDestroyCell(this);
            return;
        }

        if (isOccupied || GameManager.Instance.isGameOver || GameManager.Instance.currentPlayer != Player.Human)
            return;

        SetSymbol("X");
        CheckEndOrSwitch();
    }

    public void SetSymbol(string sym)
    {
        symbol = sym;
        isOccupied = true;

        UpdateVisual();

        GameManager.Instance.RecordMove(x, y, sym, GameManager.Instance.currentPlayer);
    }

    public void SetSymbolSimulate(string sym)
    {
        symbol = sym;
        isOccupied = true;

        UpdateVisual();
    }

    public void Reset()
    {
        symbol = "";
        isOccupied = false;
        isLocked = false;
        lockForAIOnly = false;

        UpdateVisual();
    }

    public void LockForAI()
    {
        Debug.Log("·âËø£¡£¡£¡£¡");
        isLocked = true;
        lockForAIOnly = true;

        UpdateVisual();
    }

    public void Unlock()
    {
        isLocked = false;
        lockForAIOnly = false;

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (isLocked)
        {
            symbolImage.sprite = lockedSprite;
        }
        else if (symbol == "X")
        {
            symbolImage.sprite = playerSprite;
        }
        else if (symbol == "O")
        {
            symbolImage.sprite = aiSprite;
        }
        else
        {
            symbolImage.sprite = emptySprite;
        }
    }

    void CheckEndOrSwitch()
    {
        if (BoardManager.Instance.CheckVictory(GameManager.Instance.currentPlayer))
        {
            GameManager.Instance.EndGame(
                GameManager.Instance.currentPlayer == Player.Human ?
                GameResult.PlayerWin : GameResult.AIWin);

            GameManager.Instance.isDoubleMoveActive = false;
            GameManager.Instance.hasUsedFirstMove = false;
            return;
        }

        if (BoardManager.Instance.IsFull())
        {
            GameManager.Instance.EndGame(GameResult.Draw);
            GameManager.Instance.isDoubleMoveActive = false;
            GameManager.Instance.hasUsedFirstMove = false;
            return;
        }

        GameManager.Instance.SwitchTurn();
    }
}
