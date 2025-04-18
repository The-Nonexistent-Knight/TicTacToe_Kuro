using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDeckManager : MonoBehaviour
{
    public static SkillDeckManager Instance;

    [Header("UI References")]
    public Transform skillPanel;
    public GameObject skillCardPrefab;

    private List<SkillCardData> allSkills = new List<SkillCardData>();
    private List<SkillCardData> currentHand = new List<SkillCardData>();
    private List<GameObject> currentUIs = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadAllSkills();
        DrawInitialHand();
    }

    void LoadAllSkills()
    {
        allSkills.Clear();
        SkillCardData[] skills = Resources.LoadAll<SkillCardData>("SkillCards");
        allSkills.AddRange(skills);
    }

    void DrawInitialHand()
    {
        currentHand.Clear();

        for (int i = 0; i < 3; i++)
        {
            SkillCardData newCard = DrawRandomSkill();
            currentHand.Add(newCard);
            CreateSkillCardUI(newCard, i);
        }
    }

    SkillCardData DrawRandomSkill()
    {
        int index = Random.Range(0, allSkills.Count);
        return allSkills[index];
    }

    public void ReplaceSkillCard(int index)
    {
        SkillCardData newCard = DrawRandomSkill();
        currentHand[index] = newCard;

        SkillCardUI ui = currentUIs[index].GetComponent<SkillCardUI>();
        ui.Setup(newCard, index);
    }



    void CreateSkillCardUI(SkillCardData data, int slotIndex)
    {
        GameObject go = Instantiate(skillCardPrefab, skillPanel);
        SkillCardUI ui = go.GetComponent<SkillCardUI>();
        ui.Setup(data, slotIndex);

        currentUIs.Add(go);
    }


    public void ReplaceAllSkillCards()
    {
        foreach (var uiObj in currentUIs)
        {
            Destroy(uiObj);
        }
        currentUIs.Clear();
        currentHand.Clear();

        for (int i = 0; i < 3; i++)
        {
            SkillCardData newCard = DrawRandomSkill();
            currentHand.Add(newCard);
            CreateSkillCardUI(newCard, i);
        }
    }


}
