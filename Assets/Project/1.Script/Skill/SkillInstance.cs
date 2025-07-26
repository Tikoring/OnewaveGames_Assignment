using UnityEngine;

public class SkillInstance
{
    public Skill skill { get; private set; }
    public float remain { get; private set; }

    public bool inActive => remain <= 0f;
    public float cooldownRatio => remain / skill.SkillData.Cooldown;

    public SkillInstance(Skill skill)
    {
        this.skill = skill;
        remain = 0;
    }

    public void TickCooldown(float deltaTime)
    {
        if (remain > 0f)
            remain = Mathf.Max(0f, remain - deltaTime);
    }

    public void UseSkill(SkillContext context)
    {
        skill.UseSkill(context);
        remain = skill.SkillData.Cooldown;
    }
}