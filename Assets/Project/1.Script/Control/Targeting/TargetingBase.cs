using UnityEngine;
using UnityEngine.Events;

public abstract class TargetingBase : ScriptableObject
{
    protected UnityAction<SkillContext> targetCallback;
    public abstract void BeginTargeting(UnityAction<SkillContext> onTargetSelected);
    public abstract void ActiveTargeting(SkillContext context, RaycastHit hit);
    public abstract void CancelTargeting();
}