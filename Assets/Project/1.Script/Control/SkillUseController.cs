using UnityEngine;

public class SkillUseController
{
    private GameObject caster;

    private SkillInstance currentSkill;
    private bool isTargeting;

    private SkillContext context;

    public SkillUseController(GameObject caster)
    {
        context = new SkillContext();
        this.caster = caster;
        isTargeting = false;
    }
    public bool TryActivateSkillTargeting(SkillInstance skill)
    {
        if (skill.inActive)
        {
            if (isTargeting) return true;
            currentSkill = skill;
            isTargeting = true;

            currentSkill.skill.TargetType.BeginTargeting(OnTargetConfirmed);
            return true;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("skill cooldown");
#endif 
            return false;
        }
    }

    public void TargetConfirm(RaycastHit hit)
    {
        if (!isTargeting) return;

        context.Clear();
        context.caster = caster;
        context.castedPosition = caster.transform.position;
        currentSkill.skill.TargetType.ActiveTargeting(context, hit);
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
            currentSkill.skill.TargetType.CancelTargeting();
            isTargeting = false;
            currentSkill = null;
        }
    }
}