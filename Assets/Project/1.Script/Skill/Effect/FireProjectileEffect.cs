using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (
        fileName  = "NewSkillEffect",
        menuName = "GaneData/SkillEffects/FireProjectile",
        order = 1
    )
]
public class FireProjectileEffect : EffectBase
{
    public override void EffectActive(SkillContext context, EffectDataBase data)
    {
        FireProjectileData realData = data as FireProjectileData;

        Projectile projectile = GameObject.Instantiate(
            realData.projectile,
            context.caster.transform.position,
            context.caster.transform.rotation
        );

        projectile.Initialize(realData.speed, context.skillData.Range, realData.effects, context);
    }
}

[CreateAssetMenu
    (
        fileName = "NewSkillEffectData",
        menuName = "GaneData/SkillEffectDatas/FireProjectileData",
        order = 1
    )
]
public class FireProjectileData : EffectDataBase
{
    public Projectile projectile;
    public float speed;
    public Vector3 offsetPos;
    public List<EffectEntry> effects;
}
