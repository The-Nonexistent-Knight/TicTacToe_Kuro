using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject resultPanel;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI resultText;
    public TMP_Text totalWinText;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateEnergyUI(int amount)
    {
        energyText.text = $"当前能量: {amount}";
    }

    public void UpdateWinUI(int wins)
    {
        totalWinText.text = $"当前胜场: {wins}";
    }

    public void ShowResult(string msg)
    {
        resultText.text = msg;
        resultPanel.SetActive(true);
        UpdateWinUI(GameManager.Instance.totalWins);
    }

    public void HideResult()
    {
        resultPanel.SetActive(false);
    }
}
