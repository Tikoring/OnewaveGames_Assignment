using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (
        fileName = "NewSkill",
        menuName = "GaneData/Skills/Skill",
        order = 1
    )
]
public class Skill : ScriptableObject
{
    [SerializeField] protected SkillData skillData;
    public SkillData SkillData { get { return skillData; }}
    [SerializeField] protected TargetingBase targetType;
    public TargetingBase TargetType { get { return targetType; } }

    [SerializeField] protected List<EffectEntry> effects;

    public virtual void UseSkill(SkillContext context)
    {
        context.skillData = skillData;
        foreach (EffectEntry effect in effects)
        {
            effect.EntryActive(context);
        }
    }
}