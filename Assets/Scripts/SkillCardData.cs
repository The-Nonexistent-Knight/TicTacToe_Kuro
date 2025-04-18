using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "SkillSystem/SkillCard")]
public class SkillCardData : ScriptableObject
{
    public string skillName;
    public int energyCost;
    public string description;
    public Sprite icon;
    public SkillEffectType effectType;
}

public enum SkillEffectType
{
    Rewind,
    SealAI,
    DestroyPiece,
    DoubleMove,
    ReplaceCard,
    LockCell
}
