using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu
    (
        fileName = "NewSkillBind",
        menuName = "GaneData/Charcter/SkillBind",
        order = 1
    )
]
public class SkillBindSet : ScriptableObject
{
    public SkillBind[] skillBinds;
}
[Serializable]
public class SkillBind
{
    public InputActionReference hotKey;
    public Skill skill;
}