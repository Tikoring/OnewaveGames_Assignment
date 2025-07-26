using UnityEngine;

public class SkillUseController
{
    private GameObject caster;

    private Skill currentSkill;
    private bool isTargeting;

    private SkillContext context;

    public SkillUseController(GameObject caster)
    {
        context = new SkillContext();
        this.caster = caster;
        isTargeting = false;
    }
    public void TryActivateSkillTargeting(Skill skill)
    {
        if (isTargeting) return;

        currentSkill = skill;
        isTargeting = true;

        skill.TargetType.BeginTargeting(OnTargetConfirmed);
    }

    public void TargetConfirm(RaycastHit hit)
    {
        if (!isTargeting) return;

        context.Clear();
        context.caster = caster;
        context.castedPosition = caster.transform.position;
        currentSkill.TargetType.ActiveTargeting(context, hit);
    }

    private void OnTargetConfirmed(SkillContext context)
    {
        isTargeting = false;
        currentSkill.UseSkill(context);
        currentSkill = null;
    }

    public void Cancel()
    {
        if (isTargeting)
        {
#if UNITY_EDITOR
        Debug.Log("skill target real cancel");
#endif
            currentSkill.TargetType.CancelTargeting();
            isTargeting = false;
            currentSkill = null;
        }
    }
}