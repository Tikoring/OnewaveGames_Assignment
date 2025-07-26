using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private int healthPoint;
    private byte abilityType;       /// health = 0, mana = 1, 필요에 따라 추가예정
    private int abilityPoint;

    private SkillSet skillSet;

    private SkillUseController skillUseController;
    private SkillInputHandler skillInputHandler;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private SkillBindSet skillBindSet;
    private InputAction skillHotKey;
    private InputAction move;
    private InputAction skillTargetConfirm;
    private InputActionMap playerMap;
    private bool isMove;
    private Vector3 targetPosition;
    private float moveSpeed = 2;
    public int AbilityPoint
    {
        get
        {
            if (abilityType == 0) { return healthPoint; }
            else { return abilityPoint; }
        }
        set
        {
            if (abilityType == 0) { healthPoint = value; }
            else { abilityPoint = value; }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        skillSet = new SkillSet();
        skillSet.Initialize(skillBindSet);
        skillUseController = new SkillUseController(gameObject);
        skillInputHandler = new SkillInputHandler(skillUseController, skillSet);

        SetInputAction();
    }

    private void SetInputAction()
    {
        skillHotKey = inputActions.FindAction("Player/Skill1");
        move = inputActions.FindAction("Player/Move");
        skillTargetConfirm = inputActions.FindAction("Player/Click");
        playerMap = inputActions.FindActionMap("Player");

        // skill
        skillInputHandler.SetSkillHotKey(skillHotKey);
        skillInputHandler.SkillTargetConfirm(skillTargetConfirm);
        skillInputHandler.SkillTargetCancel(playerMap, skillTargetConfirm);

        // move mouse button
        move.performed += OnMovePerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 nextPos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(nextPos);
        LayerMask myLayerMask = LayerMask.GetMask("Field");
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, myLayerMask))
        {
            targetPosition = hit.point;
            targetPosition.y += 1;
            isMove = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMove = false;
            }
        }
    }
}
