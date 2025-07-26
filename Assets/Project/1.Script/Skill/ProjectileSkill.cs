using UnityEngine;

[CreateAssetMenu
    (
        fileName = "NewProjectileSkill",
        menuName = "GaneData/Skills/ProjectileSkill",
        order = 1
    )
]
public class ProjectileSkill : Skill
{
    public override void UseSkill(SkillContext context)
    {
        base.UseSkill(context);
        foreach (EffectEntry effect in effects)
        {
            effect.EntryActive(context);
        }
    }
}