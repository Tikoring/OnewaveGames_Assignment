using System;
using UnityEngine;

[CreateAssetMenu
    (
        fileName  = "NewSkillEffect",
        menuName = "GaneData/SkillEffects/FaceTarget",
        order = 1
    )
]
public class FaceTargetEffect : EffectBase
{
    public override void EffectActive(SkillContext context, EffectDataBase  data)
    {
#if UNITY_EDITOR
        Debug.Log(string.Format("cur: {0}, next: {1}", context.caster.transform.forward, context.direction));
#endif 
        context.caster.transform.rotation = Quaternion.FromToRotation
        (
            context.caster.transform.forward,
            context.direction
        );

    }
}

