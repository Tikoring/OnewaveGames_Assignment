using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : ScriptableObject
{
    [SerializeField] protected SkillData skillData;
    public SkillData SkillData { get { return skillData; }}
    [SerializeField] protected TargetingBase targetType;
    public TargetingBase TargetType { get { return targetType; } }

    [SerializeField] protected List<EffectEntry> effects;

    public virtual void UseSkill(SkillContext context)
    {
        context.skillData = skillData;
    }
}