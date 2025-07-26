using System.Collections.Generic;
using UnityEngine.InputSystem;

public struct SkillHotKeyBinding
{
    public InputAction action;
    public SkillInstance skill;
}

public class SkillSet
{
    private Dictionary<string, SkillInstance> hotKeyMap;

    public void Initialize(SkillBindSet bindSet)
    {
        hotKeyMap = new Dictionary<string, SkillInstance>();
        foreach (var bind in bindSet.skillBinds)
        {
            hotKeyMap[bind.hotKey.name] = new SkillInstance(bind.skill);
        }
    }

    public void TickCooldown(float deltaTime)
    {
        foreach (var value in hotKeyMap.Values)
        {
            value.TickCooldown(deltaTime);
        }
    }

    public SkillInstance GetSkillInstance(InputAction action)
    {
        string fullName = $"{action.actionMap.name}/{action.name}";
        hotKeyMap.TryGetValue(fullName, out SkillInstance skill);

        return skill;
    }
}