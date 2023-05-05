using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public SkillType type;
    public int cost;
    public int value;
    public SkillSlot previousNeededSkill;
}
public enum SkillType
{
    HEALTH, ATTACK, ARMOR, SPEED
}