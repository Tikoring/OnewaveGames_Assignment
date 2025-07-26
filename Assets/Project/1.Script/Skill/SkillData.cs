using UnityEngine;

[CreateAssetMenu
    (
        fileName = "NewSkillData",
        menuName = "GaneData/Skills/SkillData",
        order = 1
    )
]
public class SkillData : ScriptableObject
{
    [SerializeField] private string skillName;
    public string SkillName { get { return skillName; } }
    [SerializeField] private float cooldown;
    public float Cooldown { get { return cooldown; } }
    [SerializeField] private float range;
    public float Range { get { return range; } }
}