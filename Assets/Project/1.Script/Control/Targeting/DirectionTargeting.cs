using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu
    (
        fileName  = "NewDirectionTargeting",
        menuName = "GaneData/Targeting/DirectionTargeting",
        order = 1
    )
]
public class DirectionTargeting : TargetingBase
{    
    public override void BeginTargeting(UnityAction<SkillContext> onTargetSelected)
    {
        targetCallback = onTargetSelected;
    }

    public override void ActiveTargeting(SkillContext context, RaycastHit hit)
    {
        Vector3 worldPos = hit.point;
        worldPos.y = context.castedPosition.y;
        context.direction = (worldPos - context.castedPosition).normalized;

        targetCallback(context);
    }

    public override void CancelTargeting()
    {
        targetCallback = null;
    }
}