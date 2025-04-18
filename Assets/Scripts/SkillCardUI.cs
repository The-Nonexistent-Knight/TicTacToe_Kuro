using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkillCardUI : MonoBehaviour
{
    public TMP_Text skillNameText;
    public TMP_Text energyCostText;
    public TMP_Text descriptionText;
    public TMP_Text energyWarningText;
    private CanvasGroup energyWarningGroup;

    private SkillCardData data;
    private int slotIndex;

    private void Start()
    {
        if (energyWarningText == null)
        {
            GameObject found = GameObject.Find("EnergyWarningText");
            if (found != null)
                energyWarningText = found.GetComponent<TMP_Text>();
        }

        if (energyWarningText != null)
        {
            energyWarningGroup = energyWarningText.GetComponent<CanvasGroup>();
            if (energyWarningGroup == null)
                energyWarningGroup = energyWarningText.gameObject.AddComponent<CanvasGroup>();

            energyWarningGroup.alpha = 0f;
        }
    }

    public void Setup(SkillCardData skillData, int index)
    {
        data = skillData;
        slotIndex = index;

        skillNameText.text = data.skillName;
        energyCostText.text = $"能量花费: {data.energyCost}";
        descriptionText.text = data.description;

        var button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (GameSession.Instance.CurrentEnergy < data.energyCost)
        {
            ShowEnergyWarning();
            return;
        }

        GameSession.Instance.UseEnergy(data.energyCost);
        SkillEffectProcessor.ApplyEffect(data.effectType);
        SkillDeckManager.Instance.ReplaceSkillCard(slotIndex);
    }

    void ShowEnergyWarning()
    {
        if (energyWarningText != null && energyWarningGroup != null)
        {
            energyWarningText.text = "能量不足……";
            energyWarningGroup.alpha = 1f;
            energyWarningGroup.DOFade(0f, 1f).SetDelay(1.2f);
        }
    }
}
