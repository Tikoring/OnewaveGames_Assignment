using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillInputHandler
{
    private SkillSet skillSet;
    private SkillUseController useController;
    private InputAction curTargetAction;

    public SkillInputHandler(SkillUseController useController, SkillSet skillSet)
    {
        this.useController = useController;
        this.skillSet = skillSet;
    }

    public void SetSkillHotKey(InputAction action)
    {
        action.performed += OnSkillPerformed;
    }

    private void OnSkillPerformed(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        Debug.Log("skill hotkey");
#endif
        useController.TryActivateSkillTargeting(skillSet.GetSkill(context.action));
        curTargetAction = context.action;
    }

    public void SkillTargetConfirm(InputAction action)
    {
        action.performed += OnSkillTargetConfirmPerformed;
    }

    private void OnSkillTargetConfirmPerformed(InputAction.CallbackContext context)
    {
        Vector2 pos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(pos);
        LayerMask myLayerMask = LayerMask.GetMask("Field", "Default");
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, myLayerMask);

#if UNITY_EDITOR
        Debug.Log("skill target confirm");
#endif

        useController.TargetConfirm(hit);
    }

    public void SkillTargetCancel(InputActionMap map, params InputAction[] excptActions)
    {
        foreach (InputAction action in map.actions)
        {
            action.performed -= OnSkillTargetCancel;
        }

        foreach (InputAction action in map.actions)
        {
            if (excptActions.Contains(action))
            {
                continue;
            }
            action.performed += OnSkillTargetCancel;
        }
    }

    private void OnSkillTargetCancel(InputAction.CallbackContext context)
    {
        if (curTargetAction != null && curTargetAction == context.action) { return; }
#if UNITY_EDITOR
        Debug.Log("skill target try cancel");
#endif
        useController.Cancel();
    }
}