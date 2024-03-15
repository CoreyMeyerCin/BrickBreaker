using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public string SkillName;
    public string SkillDescription;
    public SkillType SkillType;
    public int SkillLevel;
    public int SkillCost;
    public Sprite SkillIcon;

} 
public enum SkillType
{
    Passive,
    Active
}
