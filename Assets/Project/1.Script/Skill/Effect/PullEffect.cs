using UnityEngine;

[CreateAssetMenu
    (
        fileName = "NewSkillEffect",
        menuName = "GaneData/SkillEffects/Pull",
        order = 1
    )
]
public class PullEffect : EffectBase
{
    public override void EffectActive(SkillContext context, EffectDataBase data)
    {
        PullEffectData realData = data as PullEffectData;

        context.target.TryGetComponent(out Dummy d);
        d.PullDummy(context.castedPosition, context.direction, realData.pullSpeed, realData.pullRatioDist);
    }
}

[CreateAssetMenu
    (
        fileName = "NewSkillEffectData",
        menuName = "GaneData/SkillEffectDatas/PullData",
        order = 1
    )
]
public class PullEffectData : EffectDataBase
{
    public float pullSpeed;
    public float pullRatioDist;
}
