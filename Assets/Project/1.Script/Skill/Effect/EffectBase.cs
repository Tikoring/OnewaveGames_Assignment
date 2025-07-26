using System;
using UnityEngine;

public abstract class EffectBase : ScriptableObject
{
    public abstract void EffectActive(SkillContext context, EffectDataBase data);
}

public abstract class EffectDataBase : ScriptableObject { }

[Serializable]
public class EffectEntry
{
    public EffectBase effect;
    public EffectDataBase effectData;

    public void EntryActive(SkillContext context)
    {
        effect.EffectActive(context, effectData);
    }
}


[CreateAssetMenu
    (
        fileName  = "EmptyData",
        menuName = "GaneData/SkillEffectDatas/EmptyData",
        order = 1
    )
]
public class EmptyEffectData : EffectDataBase
{
}