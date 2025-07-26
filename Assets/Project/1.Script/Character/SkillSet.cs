using System.Collections.Generic;
using UnityEngine.InputSystem;

public struct SkillHotKeyBinding
{
    public InputAction action;
    public Skill skill;
}

public class SkillSet
{
    private Dictionary<string, Skill> hotKeyMap;

    public void Initialize(SkillBindSet bindSet)
    {
        hotKeyMap = new Dictionary<string, Skill>();
        foreach (var bind in bindSet.skillBinds)
        {
            hotKeyMap[bind.hotKey.name] = bind.skill;
        }
    }

    public Skill GetSkill(InputAction action)
    {
        string fullName = string.Concat(action.actionMap.name, "/", action.name);
        hotKeyMap.TryGetValue(fullName, out Skill skill);
        return skill;
    }
}