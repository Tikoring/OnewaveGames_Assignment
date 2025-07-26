using UnityEngine;
using UnityEngine.Events;

public class SkillContext
{
    public GameObject caster;
    public Vector3 castedPosition;
    public Vector3 direction;
    public GameObject target;
    public SkillData skillData;

    public void Clear()
    {
        caster = null;
        target = null;
        direction = Vector3.zero * -5000;
        castedPosition = Vector3.zero * -5000;
        skillData = null;
    }
}