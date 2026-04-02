using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public enum CharacterState
{
    Idle,
    Walking,
    Running,
    ExecutingAction
}

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Animator GetAnimator() => animator;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private @InputSystem_Actions inputActions;
    private Vector2 moveInput;

    [SerializeField] private float moveSpeed = 5f;

    private Tile HoveredTile;

    [SerializeField]
    private int interactingRange = 1;

    private void Awake()
    {

        inputActions = new @InputSystem_Actions();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        stateMachine.GoIdle();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Action.performed += OnAction;
    }

    private void OnDisable()
    {
        inputActions.Player.Action.performed -= OnAction;
        inputActions.Player.Disable();
    }

    private void DetectMove()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        if (!CanMove()) return;

        if (moveInput != Vector2.zero)
        {
            stateMachine.GoWalking();
        }
        else
        {
            stateMachine.GoIdle();
        }
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        // Handle action input (e.g., attack, interact)
        Debug.Log("Action performed!");

        if (HoveredTile != null)
        {
            Tile characterTile = GetTileUnderCharacter();
            if (IsTileWithingInteractionRange(characterTile, HoveredTile))
            {
                stateMachine.GoExecutingAction();
                HoveredTile.Interact();
            }
            else
            {
                Debug.Log("Target tile is out of interaction range.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHoveredTile();
        DetectMove();
        switch (stateMachine.GetState())
        {
            case CharacterState.Walking:
                ManageMovement();
                break;
            case CharacterState.ExecutingAction:
            case CharacterState.Idle:
            case CharacterState.Running:
                break;
        }
    }

    private void UpdateHoveredTile()
    {
        Tile newHoveredTile = GetTileUnderMouse();
        if (newHoveredTile != HoveredTile)
        {
            if (HoveredTile != null)
            {
                HoveredTile.SetColor(Color.white); // Reset color of previously hovered tile
            }
            HoveredTile = newHoveredTile;
            if (HoveredTile != null)
            {
                HoveredTile.SetColor(Color.yellow); // Highlight new hovered tile
            }
        }
    }

    void ManageMovement()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (moveInput != Vector2.zero)
        {
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private bool CanMove()
    {
        return stateMachine.GetState() == CharacterState.Idle || stateMachine.GetState() == CharacterState.Walking;
    }

    public void FinishAction()
    {
        // This method can be called by animation events to signal the end of an action
        Debug.Log("Action finished!");
        stateMachine.GoIdle();
    }

    public Tile GetTileUnderMouse()
    {
        // Get mouse position using Input System
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // Convert to world coordinates - try different approaches
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));
        mouseWorldPos.z = 0;

        Tile tile = MapManager._instance.GetTileAtWorldPosition(mouseWorldPos);

        return tile;
    }

    public Tile GetTileUnderCharacter()
    {
        Vector3 characterWorldPos = transform.position;
        Tile tile = MapManager._instance.GetTileAtWorldPosition(characterWorldPos);
        return tile;
    }

    public bool IsTileWithingInteractionRange(Tile characterTile, Tile InteractingTile)
    {
        Vector2 characterPos = characterTile.GetPosition();
        Vector2 interactingPos = InteractingTile.GetPosition();
        return math.abs(characterPos.x - interactingPos.x) <= interactingRange && math.abs(characterPos.y - interactingPos.y) <= interactingRange;
    }
}
